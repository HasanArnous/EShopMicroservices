using Discount.gRPC.Data;
using Discount.gRPC.Models;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.gRPC.Services;

public class DiscountService
	(DiscountDbContext db, ILogger<DiscountService> logger)
	: DiscountProtoService.DiscountProtoServiceBase
{
	public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
	{
		if(Guid.TryParse(request.ProductId, out Guid productId))
			throw new RpcException(status: new Status(StatusCode.InvalidArgument, "Invalid Product ID"));
		var coupon = await db.Coupones.FirstOrDefaultAsync(c => c.ProductId == productId);
		if(coupon == null) 
			coupon = new Coupon {Id = -1, ProductId = productId, Description = "No Coupone for this Product!", Amount = 0 };
		logger.LogInformation("Coupone for {ProductId}, Amount: {CuoponAmount}", request.ProductId, coupon.Amount);
		var couponModel = coupon.Adapt<CouponModel>();
		return couponModel;
	}

	public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
	{
		var coupon = request.Coupon.Adapt<Coupon>();
		if (coupon == null)
			throw new RpcException(status: new Status(StatusCode.InvalidArgument, "Invalid Coupon detials"));
		db.Coupones.Add(coupon);
		await db.SaveChangesAsync();
		logger.LogInformation("Coupons - Create - Success - Coupon Id: {CouponId}, ProductId: {ProductId}", coupon.Id, coupon.ProductId);
		return coupon.Adapt<CouponModel>();
	}

	public async override Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
	{
		var coupon = request.Coupon.Adapt<Coupon>();
		if (coupon == null)
			throw new RpcException(status: new Status(StatusCode.InvalidArgument, "Invalid Coupon detials"));
		if (await db.Coupones.CountAsync(c => c.Id == request.Coupon.Id) == 0)
		{
			logger.LogWarning("Coupones - Update - NOT FOUND - Coupone Id: {CouponId}", request.Coupon.Id);
			throw new RpcException(new Status(StatusCode.NotFound, "No Discount Found!"));
		}
		db.Coupones.Update(coupon);
		await db.SaveChangesAsync();
		logger.LogInformation("Coupons - Update - Success - Coupon Id: {CouponId}, ProductId: {ProductId}", coupon.Id, coupon.ProductId);
		return coupon.Adapt<CouponModel>();
	}

	public async override Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
	{
		if(Guid.TryParse(request.ProductId, out Guid productId))
			throw new RpcException(status: new Status(StatusCode.InvalidArgument, "Invalid Product ID"));
		var coupon = await db.Coupones.FirstOrDefaultAsync(c => c.ProductId == productId);
		if (coupon == null)
		{
			logger.LogWarning("Coupones - DELETE - NOT FOUND - Coupone Id: {CouponId}", productId);
			throw new RpcException(new Status(StatusCode.NotFound, "No Discount Found!"));
		}
		db.Coupones.Remove(coupon);
		await db.SaveChangesAsync();
		return new DeleteDiscountResponse { IsSuccess = true };
	}
}
