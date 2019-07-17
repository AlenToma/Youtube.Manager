CREATE OR ALTER  PROCEDURE User_Suggestion
	 @UserId bigint,
	 @PageSize int
AS
BEGIN
	SET NOCOUNT ON;
		select * from(
		select cat.*, 
		(select count(*) from VideoData where cat.EntityId = VideoData.Category_Id) as TotalVideos,
		(select count(*) from Rating where  RatingType = 'Up' AND Category_Id = cat.EntityId) as Up_Vote,
		(select COUNT(*) from Rating where  Category_Id = cat.EntityId and Ratingtype = 'Down') as Down_Vote,
		(select count(Rating.EntityId) from Rating
		inner join VideoData vd on vd.Category_Id = cat.EntityId and vd.EntityId = Rating.VideoData_Id
		where  RatingType = 'Up' AND vd.EntityId = vd.EntityId
		group by Rating.EntityId
		) as VideoDataRating
		 from VideoCategory cat
		left join VideoData v on v.Category_Id = cat.EntityId
		where (select top 1 1 from UserSearch us where us.User_Id = @UserId and v.Title like '%'+ us.Text +'%') = 1 and cat.User_Id != @UserId
		group by cat.EntityId, cat.Name, cat.Logo, cat.User_Id
		UNION
		select cat.*,
		(select count(*) from VideoData where cat.EntityId = VideoData.Category_Id) as TotalVideos,
		(select count(*) from Rating where  RatingType = 'Up' AND Category_Id = cat.EntityId) as Up_Vote,
		(select COUNT(*) from Rating where  Category_Id = cat.EntityId and Ratingtype = 'Down') as Down_Vote,
		(select count(Rating.EntityId) from Rating
		inner join VideoData vd on vd.Category_Id = cat.EntityId and vd.EntityId = Rating.VideoData_Id
		where  RatingType = 'Up' AND vd.EntityId = vd.EntityId
		group by Rating.EntityId
		) as VideoDataRating
		 from VideoCategory cat
		left join VideoData v on v.Category_Id = cat.EntityId
		where cat.User_Id != @UserId
		group by cat.EntityId, cat.Name, cat.Logo, cat.User_Id
		) as t order by  Up_Vote  desc, VideoDataRating desc OFFSET 0 ROWS FETCH NEXT @PageSize ROWS ONLY 
END