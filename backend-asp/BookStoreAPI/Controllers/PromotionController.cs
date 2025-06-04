using BookStoreAPI.Common.Controllers;
using BookStoreAPI.Services.PromotionService.DTOs;
using BookStoreAPI.Services.PromotionService.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers
{
    public class PromotionController : BaseController
    {
        private readonly IPromotionService _promotionService;

        public PromotionController(IPromotionService promotionService)
        {
            _promotionService = promotionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _promotionService.GetAllAsync();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _promotionService.GetByIdAsync(id);

            return Ok(result);
        }
        [HttpGet("[Action]/{id}")]
        public async Task<IActionResult> UpdateOrder(Guid id)
        {
            try
            {
                var promotion = await _promotionService.GetByIdAsync(id);
                if (promotion.Quantity < 1)
                {
                    return BadRequest(new { message = "The book does not have enough quantity." });
                }
                PromotionUpdateDTO updatePromotion = new PromotionUpdateDTO
                {
                    Name = promotion.Name,
                    StartDate = promotion.StartDate,
                    EndDate = promotion.EndDate,
                    Condition = promotion.Condition,
                    DiscountPercent = promotion.DiscountPercent,
                    Quantity = (short)(promotion.Quantity - 1),
                    IsDeleted = promotion.IsDeleted
                };
                await _promotionService.UpdateAsync(promotion.Id, updatePromotion,false);
                return Ok(new { message = promotion.DiscountPercent.ToString() });
            }
            catch (Exception ex) {
                return BadRequest(new { message = "Error when update." });
            }
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchByKeyword([FromQuery] string? keyword)
        {
            var result = await _promotionService.SearchByKeywordAsync(keyword);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] PromotionCreateDTO promotionCreateDTO)
        {
            var result = await _promotionService.AddAsync(promotionCreateDTO);

            return Ok(new { message = "Promotion created successfully." });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] PromotionUpdateDTO promotionUpdateDTO)
        {
            var result = await _promotionService.UpdateAsync(id, promotionUpdateDTO, true);

            return Ok(new { message = "Promotion updated successfully." });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _promotionService.DeleteAsync(id);

            return Ok(new { message = "Promotion soft deleted successfully." });
        }
    }
}
