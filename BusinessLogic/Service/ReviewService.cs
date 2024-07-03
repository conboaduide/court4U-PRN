using BusinessLogic.Service.Interface;
using DataAccess.Entity.Data;
using DataAccess.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Service
{
    public class ReviewService : IReviewService
    {
        IReviewRepository iReviewRepository;

        public ReviewService(IReviewRepository iReviewRepository)
        {
            this.iReviewRepository = iReviewRepository;
        }

        public async Task<Review?> Create(Review entity)
        {
            return await iReviewRepository.Create(entity);
        }

        public async Task<Review?> Delete(string id)
        {
            return await iReviewRepository.Delete(id);
        }

        public async Task<Review?> Get(string id)
        {
            return await iReviewRepository.Get(id);
        }

        public async Task<List<Review>?> Get()
        {
            return await iReviewRepository.Get();
        }

        public async Task<Review?> Update(Review entity)
        {
            return await iReviewRepository.Update(entity);
        }
    }
}
