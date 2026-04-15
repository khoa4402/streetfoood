-- Kiểm tra và chỉ thêm nếu chưa có cột
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[PointsOfInterest]') AND name = 'QrCodeId')
    ALTER TABLE PointsOfInterest ADD QrCodeId NVARCHAR(MAX) NULL;

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[PointsOfInterest]') AND name = 'ImageUrl')
    ALTER TABLE PointsOfInterest ADD ImageUrl NVARCHAR(MAX) NULL;

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[PointsOfInterest]') AND name = 'MapLink')
    ALTER TABLE PointsOfInterest ADD MapLink NVARCHAR(MAX) NULL;

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[PointsOfInterest]') AND name = 'AudioContent')
    ALTER TABLE PointsOfInterest ADD AudioContent NVARCHAR(MAX) NULL;

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[PointsOfInterest]') AND name = 'AudioFileName')
    ALTER TABLE PointsOfInterest ADD AudioFileName NVARCHAR(MAX) NULL;