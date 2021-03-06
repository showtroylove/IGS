SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE TABLE [dbo].[Info](
	[DataVersion] [int] NULL
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[Tickers](
	[Ticker] [nvarchar](5) NOT NULL,
	[Description] [nvarchar](255) NOT NULL,
	[Type] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_Companies] PRIMARY KEY CLUSTERED 
(
	[Ticker] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[DailyData](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Date] [smalldatetime] NOT NULL,
	[Open] [float] NOT NULL,
	[High] [float] NOT NULL,
	[Low] [float] NOT NULL,
	[Close] [float] NOT NULL,
	[Volume] [int] NOT NULL,
	[Ticker] [nvarchar](5) NOT NULL,
 CONSTRAINT [PK_DailyData_1] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE UNIQUE NONCLUSTERED INDEX [Data_Ticker] ON [dbo].[DailyData] 
(
	[Ticker] ASC,
	[Date] DESC
)
INCLUDE ( [Close],
[Volume]) WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Volumes](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Date] [datetime] NOT NULL,
	[Ticker] [nvarchar](5) NOT NULL,
	[Price] [float] NOT NULL,
	[Volume] [int] NOT NULL,
	[PotentialProfit] [float] NOT NULL,
 CONSTRAINT [PK_Volumes] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [Volumes_Date_Ticker] ON [dbo].[Volumes] 
(
	[Ticker] ASC,
	[Date] ASC
)
INCLUDE ( [Price],
[Volume]) WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Trades](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Ticker] [nvarchar](5) NOT NULL,
	[Price] [float] NOT NULL,
	[Volume] [int] NOT NULL,
	[Date] [datetime] NOT NULL,
 CONSTRAINT [PK_Trades] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[TradeData](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Date] [datetime] NOT NULL,
	[Ticker] [nvarchar](5) NOT NULL,
	[Volume] [int] NOT NULL,
	[FixedProfit] [float] NOT NULL,
	[PotentialProfit] [float] NOT NULL,
 CONSTRAINT [PK_TradeData] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[RealTimePosition](
	[Date] [datetime] NULL
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[RealTimeData](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Ticker] [nvarchar](5) NOT NULL,
	[Date] [datetime] NOT NULL,
	[Last] [float] NOT NULL,
	[Volume] [int] NOT NULL
) ON [PRIMARY]
GO

CREATE PROCEDURE UpdateHistoricalData 
AS
BEGIN   
   declare @shift int
   select @shift = datediff(dd, max(Date), getdate()) from DailyData
   update DailyData set Date = dateadd(dd, @shift, Date)
   update Trades set Date = dateadd(dd, @shift, Date)
END
GO

CREATE procedure [dbo].[CalculateProfitLoss]
as
declare @date datetime
declare @close float
declare @ticker nvarchar(5)
declare  @price float, @buyOrSellVolume float
set nocount on

truncate table Volumes
truncate table TradeData
declare cursor1 cursor for
select DailyData.[Date], [Close], DailyData.Ticker, price, Trades.volume from DailyData
left join Trades on Trades.Date = DailyData.Date and Trades.Ticker = DailyData.Ticker
where DailyData.Date> (select MIN(date) from Trades)
order by DailyData.[Date], Ticker


declare @profitLossTable table (ticker nvarchar(5), profitLoss float)

insert into @profitLossTable select ticker, 0 from Tickers
open cursor1

declare @assets table (id int identity(1,1) not null,  date datetime, ticker nvarchar(5), price float, volume int)
fetch next from cursor1 into @date, @close, @ticker, @price, @buyOrSellVolume
while @@FETCH_STATUS=0
begin
	declare @profitLoss float
	select @profitloss = profitLoss from  @profitLossTable where ticker = @ticker
	if (@buyOrSellVolume>0) 
	begin	    
		insert into @assets (ticker,date, price, volume) values (@ticker,@date, @price, @buyOrSellVolume)	
	end
	if (@buyOrSellVolume <0 )
	begin	
		set @buyOrSellVolume = -@buyOrSellVolume    	   
		declare @id int 
		select top 1 @id =id
		from @assets a2
		where (select SUM(volume) from @assets a1 where  a1.id<= a2.id and a1.ticker = a2.ticker) >= @buyOrSellVolume  and ticker = @ticker
		order by id 
		if @id is not null 
		begin	
			select @profitLoss = @profitLoss + isnull(SUM( volume * (@price - price)),0), @buyOrSellVolume = @buyOrSellVolume - isnull(SUM(volume),0) from @assets where  id< @id and ticker = @ticker						
			select @profitLoss = @profitLoss + @buyOrSellVolume *  (@price - price) from @assets where id = @id		
			update @assets set volume = volume - @buyOrSellVolume where id = @id
			delete from @assets where (id< @id and ticker = @ticker) or volume = 0		
			if not exists(select * from @assets where ticker = @ticker) 
				insert into @assets (date,ticker, price, volume) values (@date, @ticker, 0, 0)
			update @profitLossTable set profitLoss =@profitLoss where ticker = @ticker			

	   end
	end	
		INSERT INTO [dbo].[TradeData]
			   ([Date] ,Ticker ,[Volume] ,PotentialProfit ,FixedProfit)
		
		select      @date, @ticker, isnull(sum(volume),0), isnull(SUM(volume)* @close - SUM(price* volume),0),  @profitloss from @assets where 		ticker =@ticker				
		insert into Volumes 
			   (Date,Ticker, Price, Volume, PotentialProfit)
			   
		select @date, @ticker, price, Volume, volume *(@close-price) from @assets
		where ticker =@ticker
		
	fetch next from cursor1 into @date, @close, @ticker, @price, @buyOrSellVolume
end

close cursor1
deallocate cursor1
GO

CREATE PROCEDURE [dbo].[RefreshRealtimeData]
AS

SET NOCOUNT ON;

begin tran

declare @date datetime, @lastDate datetime

select @date = [date] from realtimeposition
    
select top 1 @lastDate   = date from DailyData order by date desc

select top 1 @date =  date from RealTimeData
where @date is null or date > @date
order by date

if @@ROWCOUNT=0
begin 
  select top 1 @date =  date from RealTimeData
  order by date
end


update DailyData
set [Close] = realtimedata.Last
from realtimedata 
where realtimedata.date = @date and DailyData.date =  @lastDate and DailyData.Ticker = RealTimeData.Ticker 

update Volumes
set PotentialProfit = (realtimedata.last - Price)* Volumes.Volume
from realtimedata
where realtimedata.date = @date and Volumes.date =  @lastDate and Volumes.Ticker = RealTimeData.Ticker 


update realtimeposition set date = @date
if(@@ROWCOUNT =0)
	insert into realtimeposition values(@date)

commit
GO

CREATE view [dbo].[CurrentAssets] as
with CloseStockValues as (SELECT top 1 with ties 
      [Close]     
      ,Ticker    
  FROM [dbo].[DailyData] dd
  order by ROW_NUMBER() over (partition by Ticker order by [date] desc))    
  select top 1 with ties  v.[Date]      
      ,v.Ticker
      ,v.Volume
      ,v.[Volume] * v.Price BuyValue
      ,v.Volume * n.[Close] SellValue      
      ,PotentialProfit from Volumes  v 
      join CloseStockValues n on v.Ticker = n.Ticker       
     order by Rank() over (partition by v.ticker order by v.date desc )
GO


CREATE view [dbo].[AssetsStructure] 
as
SELECT 'Loss' Category,  - sum(SellValue - BuyValue)  Value  
FROM [dbo].[CurrentAssets]
where  SellValue - BuyValue <0 
union
SELECT 'Profit' Category,  sum(SellValue - BuyValue) Value   
FROM [dbo].[CurrentAssets]
where  SellValue - BuyValue>0
GO

CREATE view [dbo].[CurrentStocks] as
with numbered as (SELECT  ROW_NUMBER() over (partition by Ticker order by [date] desc) number
      ,[Date]
      ,[Open]
      ,[High]
      ,[Low]
      ,[Close]
      ,[Volume]
      ,Ticker    
  FROM [dbo].[DailyData] dd )
  select n.[Date] ,n.[Open] ,n.[High] ,n.[Low] ,n.[Close] ,n.Ticker, 
		case when BuyValue<SellValue then 'Profit'
			  when BuyValue>SellValue then 'Loss' else '' end Category from numbered n
    left join CurrentAssets ca on n.Ticker = ca.Ticker
  where number = 1
GO

ALTER TABLE [dbo].[RealTimeData]  WITH CHECK ADD  CONSTRAINT [FK_RealTimeData_Tickers] FOREIGN KEY([Ticker])
REFERENCES [dbo].[Tickers] ([Ticker])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[RealTimeData] CHECK CONSTRAINT [FK_RealTimeData_Tickers]
GO


ALTER TABLE [dbo].[TradeData] ADD  CONSTRAINT [DF_TradeData_PotentialProfit]  DEFAULT ((0)) FOR [PotentialProfit]
GO

ALTER TABLE [dbo].[Volumes] ADD  CONSTRAINT [DF_Volumes_PotentialProfit]  DEFAULT ((0)) FOR [PotentialProfit]
GO

ALTER TABLE [dbo].[TradeData]  WITH CHECK ADD  CONSTRAINT [FK_TradeData_Companies] FOREIGN KEY([Ticker])
REFERENCES [dbo].[Tickers] ([Ticker])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[TradeData] CHECK CONSTRAINT [FK_TradeData_Companies]
GO

ALTER TABLE [dbo].[Trades]  WITH CHECK ADD  CONSTRAINT [FK_Trades_Companies] FOREIGN KEY([Ticker])
REFERENCES [dbo].[Tickers] ([Ticker])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Trades] CHECK CONSTRAINT [FK_Trades_Companies]
GO

ALTER TABLE [dbo].[Volumes]  WITH CHECK ADD  CONSTRAINT [FK_Volumes_Companies] FOREIGN KEY([Ticker])
REFERENCES [dbo].[Tickers] ([Ticker])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Volumes] CHECK CONSTRAINT [FK_Volumes_Companies]
GO

INSERT [dbo].[Tickers] ([Ticker], [Description], [Type]) VALUES (N'AAPL', N'Apple Inc.', N'Technology')
INSERT [dbo].[Tickers] ([Ticker], [Description], [Type]) VALUES (N'ADBE', N'Adobe Systems Inc.', N'Technology')
INSERT [dbo].[Tickers] ([Ticker], [Description], [Type]) VALUES (N'AIG', N'American International Group', N'Financial')
INSERT [dbo].[Tickers] ([Ticker], [Description], [Type]) VALUES (N'AMT', N'American Tower Corporation', N'Financial')
INSERT [dbo].[Tickers] ([Ticker], [Description], [Type]) VALUES (N'AXP', N'American Express Company', N'Financial')
INSERT [dbo].[Tickers] ([Ticker], [Description], [Type]) VALUES (N'BA', N'The Boeing Company', N'Industrial Goods')
INSERT [dbo].[Tickers] ([Ticker], [Description], [Type]) VALUES (N'BAC', N'Bank of America Corporation', N'Financial')
INSERT [dbo].[Tickers] ([Ticker], [Description], [Type]) VALUES (N'C', N'Citigroup Inc.', N'Financial')
INSERT [dbo].[Tickers] ([Ticker], [Description], [Type]) VALUES (N'GOOG', N'Google Inc.', N'Technology')
INSERT [dbo].[Tickers] ([Ticker], [Description], [Type]) VALUES (N'HPQ', N'Hewlett-Packard Company', N'Technology')
INSERT [dbo].[Tickers] ([Ticker], [Description], [Type]) VALUES (N'IBM', N'International Business Machines Corporation', N'Technology')
INSERT [dbo].[Tickers] ([Ticker], [Description], [Type]) VALUES (N'JPM', N'JPMorgan Chase & Co.', N'Financial')
INSERT [dbo].[Tickers] ([Ticker], [Description], [Type]) VALUES (N'UTX', N'United Technologies Corp.', N'Industrial Goods')
INSERT [dbo].[Tickers] ([Ticker], [Description], [Type]) VALUES (N'VZ', N'Verizon Communications Inc.', N'Technology')
INSERT [dbo].[Tickers] ([Ticker], [Description], [Type]) VALUES (N'YHOO', N'Yahoo! Inc.', N'Technology')
GO

INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'ADBE', 34.76, 1000, CAST(0x0000A11D00000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'ADBE', 38.13, 1000, CAST(0x0000A17200000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'ADBE', 38.07, 1000, CAST(0x0000A12C00000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'ADBE', 36.96, -1000, CAST(0x0000A13500000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'ADBE', 37.54, -1000, CAST(0x0000A14A00000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'BAC', 10, 1000, CAST(0x0000A11E00000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'BAC', 8.45, 1000, CAST(0x0000A0C500000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'BAC', 8.76, -500, CAST(0x0000A0D800000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'BAC', 12.01, 500, CAST(0x0000A13A00000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'BAC', 11.23, -500, CAST(0x0000A14900000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'BAC', 11.17, -500, CAST(0x0000A14A00000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'BAC', 11.04, 1000, CAST(0x0000A17000000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'BAC', 11.06, 500, CAST(0x0000A17400000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'C', 29.58, 1000, CAST(0x0000A0B300000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'C', 29.14, -500, CAST(0x0000A0B700000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'C', 30.08, 500, CAST(0x0000A0C400000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'C', 36.19, 500, CAST(0x0000A0EB00000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'C', 38.18, 500, CAST(0x0000A0ED00000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'C', 36.59, -500, CAST(0x0000A0F600000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'C', 36.09, -500, CAST(0x0000A10300000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'C', 36.09, -500, CAST(0x0000A10300000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'C', 34.15, 500, CAST(0x0000A11D00000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'C', 43.39, 500, CAST(0x0000A16300000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'C', 43.99, 1000, CAST(0x0000A16500000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'C', 43.45, -1000, CAST(0x0000A16B00000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'C', 42.06, -500, CAST(0x0000A16C00000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'GOOG', 680.1, 100, CAST(0x0000A0BC00000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'GOOG', 680.1, 100, CAST(0x0000A0BC00000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'GOOG', 677.96, -100, CAST(0x0000A0C200000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'GOOG', 714.35, 100, CAST(0x0000A0D000000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'GOOG', 751.1, -100, CAST(0x0000A0E500000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'GOOG', 722.2, -100, CAST(0x0000A0EE00000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'GOOG', 685.81, 100, CAST(0x0000A12000000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'VZ', 38.11, 1000, CAST(0x00009FF000000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'VZ', 37.96, 1000, CAST(0x0000A00000000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'VZ', 37.36, 1000, CAST(0x0000A03200000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'VZ', 44.4, 1000, CAST(0x0000A07F00000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'VZ', 42.9, -1000, CAST(0x0000A0B400000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'VZ', 41.8, -1000, CAST(0x0000A10A00000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'AAPL', 591.38, 100, CAST(0x0000A08200000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'AAPL', 582.49, 100, CAST(0x0000A09B00000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'AAPL', 623.75, 100, CAST(0x0000A0AC00000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'AAPL', 652.57, -100, CAST(0x0000A0E100000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'AAPL', 549.05, -100, CAST(0x0000A10300000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'AIG', 28.91, 1000, CAST(0x0000A02000000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'AIG', 33.65, 1000, CAST(0x0000A03F00000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'AIG', 30.88, -1000, CAST(0x0000A05300000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'AIG', 28.33, -1000, CAST(0x0000A06300000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'AIG', 32.82, 1000, CAST(0x0000A0AC00000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'AMT', 75.48, 500, CAST(0x0000A12500000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'AMT', 78.23, -500, CAST(0x0000A15600000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'AXP', 60.08, 1000, CAST(0x0000A15C00000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'AXP', 63.19, 1000, CAST(0x0000A17800000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'HPQ', 25.11, 1000, CAST(0x0000A04400000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'HPQ', 24.69, -1000, CAST(0x0000A04600000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'HPQ', 23.85, -1000, CAST(0x0000A04A00000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'HPQ', 16.79, -1000, CAST(0x0000A0DF00000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'HPQ', 14.52, 1000, CAST(0x0000A12600000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'HPQ', 13.19, 1000, CAST(0x0000A11D00000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'JPM', 37.09, 500, CAST(0x0000A0C300000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'JPM', 41.84, 500, CAST(0x0000A12000000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'IBM', 208.17, 200, CAST(0x0000A0DD00000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'IBM', 208.49, 200, CAST(0x0000A0EB00000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'IBM', 200.08, -200, CAST(0x0000A0ED00000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'IBM', 193.83, 200, CAST(0x0000A12500000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'IBM', 190.54, -300, CAST(0x0000A13500000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'IBM', 207.77, 200, CAST(0x0000A0DA00000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'IBM', 193.35, -200, CAST(0x0000A0F200000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'IBM', 199.92, -100, CAST(0x0000A15E00000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'AMT', 75.94, 1000, CAST(0x0000A12700000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'AMT', 79.09, 1000, CAST(0x0000A14700000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'AMT', 75.9, -1000, CAST(0x0000A15700000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'AMT', 73.98, -1000, CAST(0x0000A16400000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'AAPL', 700.02, 100, CAST(0x0000A0D100000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'AAPL', 682.28, 100, CAST(0x0000A0CB00000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'AIG', 35.95, 1000, CAST(0x0000A0E700000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'AIG', 36.3, 1000, CAST(0x0000A0EC00000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'AIG', 37.01, 1000, CAST(0x0000A0EE00000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'AIG', 34.64, -1000, CAST(0x0000A0F500000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'AIG', 32.93, -1000, CAST(0x0000A0FD00000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'AIG', 31.98, -1000, CAST(0x0000A10700000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'AIG', 31.12, -1000, CAST(0x0000A05200000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'YHOO', 16.03, 1000, CAST(0x0000A09B00000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'YHOO', 16.05, 1000, CAST(0x0000A0A500000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'YHOO', 15.14, -1000, CAST(0x0000A0A900000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'YHOO', 14.63, -1000, CAST(0x0000A0BE00000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'YHOO', 23.03, 1000, CAST(0x0000A17900000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'AAPL', 640.05, -100, CAST(0x0000A0E400000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'AAPL', 684.11, -100, CAST(0x0000A0D700000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'AIG', 34.07, 1000, CAST(0x0000A04000000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'AXP', 55.06, 1000, CAST(0x0000A01400000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'AXP', 58.43, 1000, CAST(0x0000A03700000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'AXP', 61.15, 1000, CAST(0x0000A04500000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'AXP', 57.94, -1000, CAST(0x0000A05300000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'AXP', 55.6, -1000, CAST(0x0000A05500000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'AXP', 54.65, -1000, CAST(0x0000A06300000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'ADBE', 34.6, 1000, CAST(0x0000A02200000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'ADBE', 33.02, -1000, CAST(0x0000A04700000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'ADBE', 32.08, 1000, CAST(0x0000A0A500000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'BAC', 8.45, 1000, CAST(0x0000A01300000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'BAC', 9.89, 1000, CAST(0x0000A02100000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'BAC', 9.45, -1000, CAST(0x0000A02700000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'IBM', 208.37, 1000, CAST(0x0000A02200000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'IBM', 205.59, -1000, CAST(0x0000A02E00000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'IBM', 194.83, 1000, CAST(0x0000A00000000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'IBM', 189.71, -1000, CAST(0x0000A10700000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'IBM', 195.69, 1000, CAST(0x0000A00000000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'IBM', 200.81, -1000, CAST(0x0000A15D00000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'AAPL', 656.07, -100, CAST(0x0000A0DE00000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'AAPL', 548.87, 100, CAST(0x0000A01200000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'AMT', 67.96, 500, CAST(0x0000A04E00000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'AMT', 67.91, 500, CAST(0x0000A05300000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'AMT', 63.83, -500, CAST(0x0000A06300000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'AMT', 69.41, -500, CAST(0x0000A0B700000000 AS DateTime))
INSERT [dbo].[Trades] ( [Ticker], [Price], [Volume], [Date]) VALUES ( N'AMT', 73.66, 500, CAST(0x0000A0A000000000 AS DateTime))

SET IDENTITY_INSERT [dbo].[RealTimeData] ON
INSERT [dbo].[RealTimeData] ([Id], [Ticker], [Date], [Last], [Volume]) VALUES (1, N'AAPL', CAST(0x0000A182009C8E20 AS DateTime), 437.97, 1100)
INSERT [dbo].[RealTimeData] ([Id], [Ticker], [Date], [Last], [Volume]) VALUES (2, N'BAC', CAST(0x0000A182009C8E20 AS DateTime), 12.53, 17600)
INSERT [dbo].[RealTimeData] ([Id], [Ticker], [Date], [Last], [Volume]) VALUES (3, N'C', CAST(0x0000A182009C8E20 AS DateTime), 47.52, 1597)
INSERT [dbo].[RealTimeData] ([Id], [Ticker], [Date], [Last], [Volume]) VALUES (4, N'HPQ', CAST(0x0000A182009C8E20 AS DateTime), 22.01, 1700)
INSERT [dbo].[RealTimeData] ([Id], [Ticker], [Date], [Last], [Volume]) VALUES (5, N'JPM', CAST(0x0000A182009C8E20 AS DateTime), 50.01, 1800)
INSERT [dbo].[RealTimeData] ([Id], [Ticker], [Date], [Last], [Volume]) VALUES (6, N'IBM', CAST(0x0000A182009C8E20 AS DateTime), 215.48, 200)
INSERT [dbo].[RealTimeData] ([Id], [Ticker], [Date], [Last], [Volume]) VALUES (7, N'GOOG', CAST(0x0000A182009C8E20 AS DateTime), 818.68, 100)
INSERT [dbo].[RealTimeData] ([Id], [Ticker], [Date], [Last], [Volume]) VALUES (8, N'GOOG', CAST(0x0000A182009C8F4C AS DateTime), 819.68, 100)
INSERT [dbo].[RealTimeData] ([Id], [Ticker], [Date], [Last], [Volume]) VALUES (9, N'IBM', CAST(0x0000A182009C8F4C AS DateTime), 215.54, 700)
INSERT [dbo].[RealTimeData] ([Id], [Ticker], [Date], [Last], [Volume]) VALUES (10, N'JPM', CAST(0x0000A182009C8F4C AS DateTime), 50.02, 3700)
INSERT [dbo].[RealTimeData] ([Id], [Ticker], [Date], [Last], [Volume]) VALUES (11, N'HPQ', CAST(0x0000A182009C8F4C AS DateTime), 22.02, 1400)
INSERT [dbo].[RealTimeData] ([Id], [Ticker], [Date], [Last], [Volume]) VALUES (12, N'BA', CAST(0x0000A182009C8F4C AS DateTime), 84.88, 200)
INSERT [dbo].[RealTimeData] ([Id], [Ticker], [Date], [Last], [Volume]) VALUES (13, N'BAC', CAST(0x0000A182009C8F4C AS DateTime), 12.52, 3700)
INSERT [dbo].[RealTimeData] ([Id], [Ticker], [Date], [Last], [Volume]) VALUES (14, N'AAPL', CAST(0x0000A182009C8F4C AS DateTime), 437.99, 200)
INSERT [dbo].[RealTimeData] ([Id], [Ticker], [Date], [Last], [Volume]) VALUES (15, N'AAPL', CAST(0x0000A182009C9078 AS DateTime), 437.8, 1340)
INSERT [dbo].[RealTimeData] ([Id], [Ticker], [Date], [Last], [Volume]) VALUES (16, N'BAC', CAST(0x0000A182009C9078 AS DateTime), 12.53, 6800)
INSERT [dbo].[RealTimeData] ([Id], [Ticker], [Date], [Last], [Volume]) VALUES (17, N'AIG', CAST(0x0000A182009C9078 AS DateTime), 38.8, 300)
INSERT [dbo].[RealTimeData] ([Id], [Ticker], [Date], [Last], [Volume]) VALUES (18, N'C', CAST(0x0000A182009C9078 AS DateTime), 47.54, 300)
INSERT [dbo].[RealTimeData] ([Id], [Ticker], [Date], [Last], [Volume]) VALUES (19, N'JPM', CAST(0x0000A182009C9078 AS DateTime), 50.03, 2600)
INSERT [dbo].[RealTimeData] ([Id], [Ticker], [Date], [Last], [Volume]) VALUES (20, N'IBM', CAST(0x0000A182009C9078 AS DateTime), 215.54, 100)
INSERT [dbo].[RealTimeData] ([Id], [Ticker], [Date], [Last], [Volume]) VALUES (21, N'IBM', CAST(0x0000A182009C91A4 AS DateTime), 215.59, 400)
INSERT [dbo].[RealTimeData] ([Id], [Ticker], [Date], [Last], [Volume]) VALUES (22, N'UTX', CAST(0x0000A182009C91A4 AS DateTime), 93.37, 774)
INSERT [dbo].[RealTimeData] ([Id], [Ticker], [Date], [Last], [Volume]) VALUES (23, N'VZ', CAST(0x0000A182009C91A4 AS DateTime), 48.21, 100)
INSERT [dbo].[RealTimeData] ([Id], [Ticker], [Date], [Last], [Volume]) VALUES (24, N'JPM', CAST(0x0000A182009C91A4 AS DateTime), 50.03, 2200)
INSERT [dbo].[RealTimeData] ([Id], [Ticker], [Date], [Last], [Volume]) VALUES (25, N'C', CAST(0x0000A182009C91A4 AS DateTime), 47.57, 300)
INSERT [dbo].[RealTimeData] ([Id], [Ticker], [Date], [Last], [Volume]) VALUES (26, N'AIG', CAST(0x0000A182009C91A4 AS DateTime), 38.8, 300)
INSERT [dbo].[RealTimeData] ([Id], [Ticker], [Date], [Last], [Volume]) VALUES (27, N'BA', CAST(0x0000A182009C91A4 AS DateTime), 84.89, 100)
INSERT [dbo].[RealTimeData] ([Id], [Ticker], [Date], [Last], [Volume]) VALUES (28, N'HPQ', CAST(0x0000A182009C91A4 AS DateTime), 22.025, 400)
INSERT [dbo].[RealTimeData] ([Id], [Ticker], [Date], [Last], [Volume]) VALUES (29, N'BAC', CAST(0x0000A182009C91A4 AS DateTime), 12.54, 3900)
INSERT [dbo].[RealTimeData] ([Id], [Ticker], [Date], [Last], [Volume]) VALUES (30, N'AAPL', CAST(0x0000A182009C91A4 AS DateTime), 437.99, 1830)
INSERT [dbo].[RealTimeData] ([Id], [Ticker], [Date], [Last], [Volume]) VALUES (31, N'BAC', CAST(0x0000A182009C92D0 AS DateTime), 12.53, 2758)
INSERT [dbo].[RealTimeData] ([Id], [Ticker], [Date], [Last], [Volume]) VALUES (32, N'HPQ', CAST(0x0000A182009C92D0 AS DateTime), 22.02, 100)
INSERT [dbo].[RealTimeData] ([Id], [Ticker], [Date], [Last], [Volume]) VALUES (33, N'AIG', CAST(0x0000A182009C92D0 AS DateTime), 38.8, 300)
INSERT [dbo].[RealTimeData] ([Id], [Ticker], [Date], [Last], [Volume]) VALUES (34, N'C', CAST(0x0000A182009C92D0 AS DateTime), 47.54, 100)
INSERT [dbo].[RealTimeData] ([Id], [Ticker], [Date], [Last], [Volume]) VALUES (35, N'GOOG', CAST(0x0000A182009C92D0 AS DateTime), 818.79, 100)
INSERT [dbo].[RealTimeData] ([Id], [Ticker], [Date], [Last], [Volume]) VALUES (36, N'IBM', CAST(0x0000A182009C92D0 AS DateTime), 215.63, 100)
INSERT [dbo].[RealTimeData] ([Id], [Ticker], [Date], [Last], [Volume]) VALUES (37, N'IBM', CAST(0x0000A182009C93FC AS DateTime), 215.63, 100)
INSERT [dbo].[RealTimeData] ([Id], [Ticker], [Date], [Last], [Volume]) VALUES (38, N'JPM', CAST(0x0000A182009C93FC AS DateTime), 49.99, 700)
INSERT [dbo].[RealTimeData] ([Id], [Ticker], [Date], [Last], [Volume]) VALUES (39, N'AXP', CAST(0x0000A182009C93FC AS DateTime), 65.6, 200)
INSERT [dbo].[RealTimeData] ([Id], [Ticker], [Date], [Last], [Volume]) VALUES (40, N'HPQ', CAST(0x0000A182009C93FC AS DateTime), 21.99, 800)
INSERT [dbo].[RealTimeData] ([Id], [Ticker], [Date], [Last], [Volume]) VALUES (41, N'BA', CAST(0x0000A182009C93FC AS DateTime), 84.81, 400)
INSERT [dbo].[RealTimeData] ([Id], [Ticker], [Date], [Last], [Volume]) VALUES (42, N'BAC', CAST(0x0000A182009C93FC AS DateTime), 12.53, 6000)
INSERT [dbo].[RealTimeData] ([Id], [Ticker], [Date], [Last], [Volume]) VALUES (43, N'AAPL', CAST(0x0000A182009C93FC AS DateTime), 438.09, 200)
INSERT [dbo].[RealTimeData] ([Id], [Ticker], [Date], [Last], [Volume]) VALUES (44, N'AAPL', CAST(0x0000A182009C9528 AS DateTime), 438.11, 300)
INSERT [dbo].[RealTimeData] ([Id], [Ticker], [Date], [Last], [Volume]) VALUES (45, N'BA', CAST(0x0000A182009C9528 AS DateTime), 84.81, 100)
INSERT [dbo].[RealTimeData] ([Id], [Ticker], [Date], [Last], [Volume]) VALUES (46, N'HPQ', CAST(0x0000A182009C9528 AS DateTime), 21.99, 1000)
INSERT [dbo].[RealTimeData] ([Id], [Ticker], [Date], [Last], [Volume]) VALUES (47, N'JPM', CAST(0x0000A182009C9528 AS DateTime), 50, 400)
INSERT [dbo].[RealTimeData] ([Id], [Ticker], [Date], [Last], [Volume]) VALUES (48, N'IBM', CAST(0x0000A182009C9528 AS DateTime), 215.68, 700)
INSERT [dbo].[RealTimeData] ([Id], [Ticker], [Date], [Last], [Volume]) VALUES (49, N'UTX', CAST(0x0000A182009C9528 AS DateTime), 93.4, 500)
INSERT [dbo].[RealTimeData] ([Id], [Ticker], [Date], [Last], [Volume]) VALUES (50, N'VZ', CAST(0x0000A182009C9528 AS DateTime), 48.24, 500)
INSERT [dbo].[RealTimeData] ([Id], [Ticker], [Date], [Last], [Volume]) VALUES (51, N'VZ', CAST(0x0000A182009C9654 AS DateTime), 48.28, 2900)
INSERT [dbo].[RealTimeData] ([Id], [Ticker], [Date], [Last], [Volume]) VALUES (52, N'JPM', CAST(0x0000A182009C9654 AS DateTime), 49.99, 1200)
INSERT [dbo].[RealTimeData] ([Id], [Ticker], [Date], [Last], [Volume]) VALUES (53, N'HPQ', CAST(0x0000A182009C9654 AS DateTime), 22.02, 900)
INSERT [dbo].[RealTimeData] ([Id], [Ticker], [Date], [Last], [Volume]) VALUES (54, N'AXP', CAST(0x0000A182009C9654 AS DateTime), 65.62, 100)
INSERT [dbo].[RealTimeData] ([Id], [Ticker], [Date], [Last], [Volume]) VALUES (55, N'AIG', CAST(0x0000A182009C9654 AS DateTime), 38.87, 600)
INSERT [dbo].[RealTimeData] ([Id], [Ticker], [Date], [Last], [Volume]) VALUES (56, N'AAPL', CAST(0x0000A182009C9654 AS DateTime), 438.49, 1000)
INSERT [dbo].[RealTimeData] ([Id], [Ticker], [Date], [Last], [Volume]) VALUES (57, N'AAPL', CAST(0x0000A182009C9780 AS DateTime), 438.39, 2000)
INSERT [dbo].[RealTimeData] ([Id], [Ticker], [Date], [Last], [Volume]) VALUES (58, N'BAC', CAST(0x0000A182009C9780 AS DateTime), 12.49, 38798)
INSERT [dbo].[RealTimeData] ([Id], [Ticker], [Date], [Last], [Volume]) VALUES (59, N'AIG', CAST(0x0000A182009C9780 AS DateTime), 38.87, 100)
INSERT [dbo].[RealTimeData] ([Id], [Ticker], [Date], [Last], [Volume]) VALUES (60, N'HPQ', CAST(0x0000A182009C9780 AS DateTime), 22.03, 500)
INSERT [dbo].[RealTimeData] ([Id], [Ticker], [Date], [Last], [Volume]) VALUES (61, N'BA', CAST(0x0000A182009C9780 AS DateTime), 84.9, 200)
INSERT [dbo].[RealTimeData] ([Id], [Ticker], [Date], [Last], [Volume]) VALUES (62, N'C', CAST(0x0000A182009C9780 AS DateTime), 47.54, 100)
INSERT [dbo].[RealTimeData] ([Id], [Ticker], [Date], [Last], [Volume]) VALUES (63, N'JPM', CAST(0x0000A182009C9780 AS DateTime), 49.99, 2581)
INSERT [dbo].[RealTimeData] ([Id], [Ticker], [Date], [Last], [Volume]) VALUES (64, N'JPM', CAST(0x0000A182009C98AC AS DateTime), 49.99, 797)
INSERT [dbo].[RealTimeData] ([Id], [Ticker], [Date], [Last], [Volume]) VALUES (65, N'GOOG', CAST(0x0000A182009C98AC AS DateTime), 819.29, 100)
INSERT [dbo].[RealTimeData] ([Id], [Ticker], [Date], [Last], [Volume]) VALUES (66, N'HPQ', CAST(0x0000A182009C98AC AS DateTime), 22.03, 100)
INSERT [dbo].[RealTimeData] ([Id], [Ticker], [Date], [Last], [Volume]) VALUES (67, N'BAC', CAST(0x0000A182009C98AC AS DateTime), 12.49, 8700)
INSERT [dbo].[RealTimeData] ([Id], [Ticker], [Date], [Last], [Volume]) VALUES (68, N'AAPL', CAST(0x0000A182009C98AC AS DateTime), 438.48, 580)
INSERT [dbo].[RealTimeData] ([Id], [Ticker], [Date], [Last], [Volume]) VALUES (69, N'AAPL', CAST(0x0000A182009C99D8 AS DateTime), 438.42, 300)
INSERT [dbo].[RealTimeData] ([Id], [Ticker], [Date], [Last], [Volume]) VALUES (70, N'BAC', CAST(0x0000A182009C99D8 AS DateTime), 12.49, 7300)
INSERT [dbo].[RealTimeData] ([Id], [Ticker], [Date], [Last], [Volume]) VALUES (71, N'JPM', CAST(0x0000A182009C99D8 AS DateTime), 49.98, 300)
INSERT [dbo].[RealTimeData] ([Id], [Ticker], [Date], [Last], [Volume]) VALUES (72, N'JPM', CAST(0x0000A182009C9B04 AS DateTime), 49.99, 100)
INSERT [dbo].[RealTimeData] ([Id], [Ticker], [Date], [Last], [Volume]) VALUES (73, N'GOOG', CAST(0x0000A182009C9B04 AS DateTime), 818.62, 100)
INSERT [dbo].[RealTimeData] ([Id], [Ticker], [Date], [Last], [Volume]) VALUES (74, N'BAC', CAST(0x0000A182009C9B04 AS DateTime), 12.49, 8100)
INSERT [dbo].[RealTimeData] ([Id], [Ticker], [Date], [Last], [Volume]) VALUES (75, N'AAPL', CAST(0x0000A182009C9B04 AS DateTime), 438.55, 939)
INSERT [dbo].[RealTimeData] ([Id], [Ticker], [Date], [Last], [Volume]) VALUES (76, N'AAPL', CAST(0x0000A182009C9C30 AS DateTime), 438.41, 1165)
INSERT [dbo].[RealTimeData] ([Id], [Ticker], [Date], [Last], [Volume]) VALUES (77, N'BAC', CAST(0x0000A182009C9C30 AS DateTime), 12.49, 9650)
INSERT [dbo].[RealTimeData] ([Id], [Ticker], [Date], [Last], [Volume]) VALUES (78, N'HPQ', CAST(0x0000A182009C9C30 AS DateTime), 22.01, 300)
INSERT [dbo].[RealTimeData] ([Id], [Ticker], [Date], [Last], [Volume]) VALUES (79, N'ADBE', CAST(0x0000A182009C9C30 AS DateTime), 41.62, 500)
INSERT [dbo].[RealTimeData] ([Id], [Ticker], [Date], [Last], [Volume]) VALUES (80, N'GOOG', CAST(0x0000A182009C9C30 AS DateTime), 818.57, 330)
INSERT [dbo].[RealTimeData] ([Id], [Ticker], [Date], [Last], [Volume]) VALUES (81, N'JPM', CAST(0x0000A182009C9D5C AS DateTime), 50, 650)
INSERT [dbo].[RealTimeData] ([Id], [Ticker], [Date], [Last], [Volume]) VALUES (82, N'C', CAST(0x0000A182009C9D5C AS DateTime), 47.54, 100)
INSERT [dbo].[RealTimeData] ([Id], [Ticker], [Date], [Last], [Volume]) VALUES (83, N'AAPL', CAST(0x0000A182009C9D5C AS DateTime), 438.47, 100)
INSERT [dbo].[RealTimeData] ([Id], [Ticker], [Date], [Last], [Volume]) VALUES (84, N'AAPL', CAST(0x0000A182009C9E88 AS DateTime), 438.41, 1484)
INSERT [dbo].[RealTimeData] ([Id], [Ticker], [Date], [Last], [Volume]) VALUES (85, N'HPQ', CAST(0x0000A182009C9E88 AS DateTime), 22.01, 100)
INSERT [dbo].[RealTimeData] ([Id], [Ticker], [Date], [Last], [Volume]) VALUES (86, N'AIG', CAST(0x0000A182009C9E88 AS DateTime), 38.87, 300)
INSERT [dbo].[RealTimeData] ([Id], [Ticker], [Date], [Last], [Volume]) VALUES (87, N'JPM', CAST(0x0000A182009C9E88 AS DateTime), 50, 800)
INSERT [dbo].[RealTimeData] ([Id], [Ticker], [Date], [Last], [Volume]) VALUES (88, N'JPM', CAST(0x0000A182009C9FB4 AS DateTime), 49.95, 460)
INSERT [dbo].[RealTimeData] ([Id], [Ticker], [Date], [Last], [Volume]) VALUES (89, N'C', CAST(0x0000A182009C9FB4 AS DateTime), 47.51, 100)
INSERT [dbo].[RealTimeData] ([Id], [Ticker], [Date], [Last], [Volume]) VALUES (90, N'BAC', CAST(0x0000A182009C9FB4 AS DateTime), 12.48, 14700)
INSERT [dbo].[RealTimeData] ([Id], [Ticker], [Date], [Last], [Volume]) VALUES (91, N'BAC', CAST(0x0000A182009CA0E0 AS DateTime), 12.48, 15600)
INSERT [dbo].[RealTimeData] ([Id], [Ticker], [Date], [Last], [Volume]) VALUES (92, N'AAPL', CAST(0x0000A182009CA0E0 AS DateTime), 438.41, 100)
INSERT [dbo].[RealTimeData] ([Id], [Ticker], [Date], [Last], [Volume]) VALUES (93, N'ADBE', CAST(0x0000A182009CA0E0 AS DateTime), 41.62, 400)
INSERT [dbo].[RealTimeData] ([Id], [Ticker], [Date], [Last], [Volume]) VALUES (94, N'JPM', CAST(0x0000A182009CA0E0 AS DateTime), 49.93, 300)
INSERT [dbo].[RealTimeData] ([Id], [Ticker], [Date], [Last], [Volume]) VALUES (95, N'GOOG', CAST(0x0000A182009CA0E0 AS DateTime), 818.53, 100)
INSERT [dbo].[RealTimeData] ([Id], [Ticker], [Date], [Last], [Volume]) VALUES (96, N'UTX', CAST(0x0000A182009CA0E0 AS DateTime), 93.39, 100)
INSERT [dbo].[RealTimeData] ([Id], [Ticker], [Date], [Last], [Volume]) VALUES (97, N'AAPL', CAST(0x0000A182009CA20C AS DateTime), 438.23, 200)
INSERT [dbo].[RealTimeData] ([Id], [Ticker], [Date], [Last], [Volume]) VALUES (98, N'BAC', CAST(0x0000A182009CA20C AS DateTime), 12.48, 11200)
INSERT [dbo].[RealTimeData] ([Id], [Ticker], [Date], [Last], [Volume]) VALUES (99, N'AAPL', CAST(0x0000A182009CA338 AS DateTime), 438.28, 1300)
INSERT [dbo].[RealTimeData] ([Id], [Ticker], [Date], [Last], [Volume]) VALUES (100, N'C', CAST(0x0000A182009CA338 AS DateTime), 47.54, 400)
SET IDENTITY_INSERT [dbo].[RealTimeData] Off
