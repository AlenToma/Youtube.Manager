select c.*,
 (select count(*) from VideoData where c.EntityId = VideoData.Category_Id) as TotalVideos,
 (select COUNT(*) from Rating where Rating.Category_Id = c.EntityId and Ratingtype = 'Up') as Up_Vote,
 (select COUNT(*) from Rating where Rating.Category_Id = c.EntityId and Ratingtype = 'Down') as Down_Vote
 from VideoCategory c
 where (c.User_Id = @UserId or @UserId is null) and (c.EntityId = @CategoryId or @CategoryId is null)