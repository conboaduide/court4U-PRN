using BusinessLogic.Service;
using BusinessLogic.Service.Interface;
using DataAccess.Entity;
using DataAccess.Entity.Data;
using DataAccess.Migrations;
using DataAccess.Repository.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;

namespace Court4U.Pages
{
    public class BookCourtModel : PageModel
    {
        private readonly ISlotService _slotService;
        private readonly IClubService _clubService;
        private readonly IBillService _billService;
        private readonly IMomoService _momoService;
        private readonly ISubscriptionOptionService _subscriptionOptionService;
        private readonly IMemberSubscriptionService _memberSubscriptionService;
        private readonly IBookingService _bookingService;
        private readonly IQRService _qrService;
        private readonly IEmailService _emailService;
        private readonly IUserService _userService;
        public BookCourtModel(ISubscriptionOptionService subscriptionOptionService, ISlotService slotService, IClubService clubService, IBillService billService, IMomoService momoService, IMemberSubscriptionService memberSubscriptionService, IBookingService bookingService, IQRService qrService, IEmailService emailService, IUserService userService)
        {
            _slotService = slotService;
            _clubService = clubService;
            _billService = billService;
            _momoService = momoService;
            _subscriptionOptionService = subscriptionOptionService;
            _memberSubscriptionService = memberSubscriptionService;
            _bookingService = bookingService;
            _qrService = qrService;
            _emailService = emailService;
            _userService = userService;
        }

        public Club SelectedClub { get; set; }
        public List<Slot> Slots { get; set; }
        public string Message { get; set; }
        [BindProperty]
        public string SelectedSlotId { get; set; }

        [BindProperty(SupportsGet = true)]
        public string ClubId { get; set; }
        [BindProperty(SupportsGet = true)]
        public DateTime SearchDate { get; set; } = DateTime.Now;
        [BindProperty]
        public List<SubscriptionOption> SubscriptionOptions { get; set; }
        [BindProperty]
        public string SubscriptionId { get; set; }
        [BindProperty]
        public DataAccess.Entity.Data.MemberSubscription MemberSubscription { get; set; }
        [BindProperty]
        public string MemberSubscriptionId { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            if (ClubId.Equals("ClubId"))
            {
                ClubId = HttpContext.Session.GetString("ClubId");
            }
            else
            {
                HttpContext.Session.SetString("ClubId", ClubId);
            }
            if (string.IsNullOrEmpty(ClubId))
            {
                return RedirectToPage("/Clubs");
            }
            var userId = HttpContext.Session.GetString("UserId");

            SelectedClub = await _clubService.GetClubByIdAsync(ClubId);
            if (SelectedClub == null)
            {
                return NotFound();
            }
            SubscriptionOptions = await _subscriptionOptionService.GetByClubId(ClubId);

            Slots = await _slotService.GetAvailableSlotsAsync(ClubId,  SearchDate);
            Slots = Slots.OrderBy(s => s.StartTime.Date).ThenBy(s => s.StartTime.TimeOfDay).ToList();
            if (userId != null)
            {
                MemberSubscription = await _memberSubscriptionService.GetByUserId(userId);
            } else
            {
                MemberSubscription = null;
            }
            return Page();
        }
        public async Task<IActionResult> OnPostSearchSlot(string ClubId, DateTime SearchDate)
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (ClubId.Equals("ClubId"))
            {
                ClubId = HttpContext.Session.GetString("ClubId");
            }
            else
            {
                HttpContext.Session.SetString("ClubId", ClubId);
            }
            if (string.IsNullOrEmpty(ClubId))
            {
                return RedirectToPage("/Clubs");
            }
            SubscriptionOptions = await _subscriptionOptionService.GetByClubId(ClubId);
            Slots = await _slotService.GetAvailableSlotsAsync(ClubId, SearchDate);
            if (userId != null)
            {
                MemberSubscription = await _memberSubscriptionService.GetByUserId(userId);
            }
            else
            {
                MemberSubscription = null;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostBuySubscriptionAsync(string SubscriptionId)
        {
            var userId = HttpContext.Session.GetString("UserId");
            if(userId == null)
            {
                return Redirect("/Login");
            }
            var subscription = await _subscriptionOptionService.Get(SubscriptionId);
            if(subscription == null)
            {
                throw new Exception("Subscription not found");
            }
            var momoRequest = new RequestCreateOrderModel()
            {
                Buy_date = DateTime.Now,
                OrderId = userId,
                Price = subscription.price,
                Type = "MemberSubscription",
                UserId = userId
            };
            var result = await _momoService.CreateMemberSubscriptionPaymentAsync(momoRequest, subscription.Id);
            return Redirect(result.PayUrl);
        }
        public async Task<IActionResult> OnPostAsync(string SelectedSlotId)
        {
            var userRole = HttpContext.Session.GetString("Role");
            if (userRole == null || userRole != "Member")
            {
                return RedirectToPage("/Login");
            }

            if (string.IsNullOrEmpty(SelectedSlotId))
            {
                Message = "Please select a slot.";
                return Page();
            }
            var slot = await _slotService.Get(SelectedSlotId);
            if (slot == null)
            {
                Message = "Slot is not available";
                return Page();
            }
            var ClubId = HttpContext.Session.GetString("ClubId");


            TempData["BookingMessage"] = Message;
            if (MemberSubscriptionId == null)
            {
                
                return Redirect($"/checkout?SelectedSlotId={SelectedSlotId}&ClubId={ClubId}&SearchDate={SearchDate}");
            } else
            {
                var userId = HttpContext.Session.GetString("UserId");

                var user = await _userService.Get(userId);
                if (user == null)
                {
                    throw new Exception("User not found");
                }
                var bill = new Bill
                {
                    Method = "Subscription",
                    Type = "Booking",
                    Price = slot.Price,
                };
                var genBill = await _billService.Create(bill);
                if(genBill == null)
                {
                    throw new Exception("Gen bill fail");
                }
                
                var booking = new Booking
                {
                    BillId = genBill.Id,
                    Price = slot.Price,
                    SlotId = slot.Id,
                    Status = true,
                    UserId = userId,
                    Date = SearchDate
                }; 
                var genBooking = await _bookingService.Create(booking);
                if(genBooking == null)
                {
                    throw new Exception("Gen booking fail");
                }
                var qrGen = _qrService.GenerateQRCode(booking.Id);
                var content = $"<h4>Date: </h4>{booking.Date}</br><h4>Price: </h4>{booking.Price}</br><img src=\"{qrGen}\"/>";
                await _emailService.SendQrCode(user.Email, content);
                return Redirect("/BookedSlot/Thanks");
            }
        }

        public async Task<IActionResult> OnGetSlot()
        {
            if (string.IsNullOrEmpty(ClubId))
            {
                return RedirectToPage("/Clubs");
            }

            SelectedClub = await _clubService.GetClubByIdAsync(ClubId);
            if (SelectedClub == null)
            {
                return NotFound();
            }

            DateTime startDate = DateTime.Today;
            DateTime endDate = DateTime.Today.AddDays(7);

            Slots = await _slotService.GetAvailableSlotsAsync(ClubId, SearchDate);
            Slots = Slots.OrderBy(s => s.StartTime.Date).ThenBy(s => s.StartTime.TimeOfDay).ToList();

            return Page();
        }
    }
}