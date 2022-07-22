using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookLibrary.Migrations
{
    public partial class ReaderStats : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			var sql = @"CREATE VIEW [dbo].[ReaderStats]
AS
WITH
_TimeToFinish AS
(
	SELECT
		br.[ReaderId],
		[TimeToFinish] = DATEDIFF(DAY, br.[StartDate], br.[EndDate]) + 1.0,
		[SpeedPg] = b.[Pages] / (DATEDIFF(DAY, br.[StartDate], br.[EndDate]) + 1.0),
		[SpeedHr] = b.[Hours] / (DATEDIFF(DAY, br.[StartDate], br.[EndDate]) + 1.0),
		b.*
	FROM
		BooksRead br INNER JOIN
		Books b ON br.[BookId] = b.[BookId]
	WHERE
		br.[StartDate] IS NOT NULL AND
		br.[EndDate] IS NOT NULL
),
_TTFFvNF2 AS (
	SELECT
		f.[ReaderId],
		[FictionAvg] = ISNULL(AVG(CASE WHEN f.[Category] = 0 THEN f.[TimeToFinish] ELSE NULL END), 0),
		[NonfictionAvg] = ISNULL(AVG(CASE WHEN f.[Category] = 1 THEN f.[TimeToFinish] ELSE NULL END), 0),
		[TotalAvg] = AVG(f.[TimeToFinish]),
		[FictionCount] = COUNT(CASE WHEN f.[Category] = 0 THEN f.[TimeToFinish] ELSE NULL END),
		[NonfictionCount] = COUNT(CASE WHEN f.[Category] = 1 THEN f.[TimeToFinish] ELSE NULL END),
		[TotalCount] = COUNT(f.[TimeToFinish]),
		[FictionPg] = SUM(CASE WHEN f.[Category] = 0 THEN f.[Pages] ELSE NULL END),
		[NonfictionPg] = SUM(CASE WHEN f.[Category] = 1 THEN f.[Pages] ELSE NULL END),
		[TotalPg] = SUM(f.[Pages]),
		[FictionHr] = SUM(CASE WHEN f.[Category] = 0 THEN f.[Hours] ELSE NULL END),
		[NonfictionHr] = SUM(CASE WHEN f.[Category] = 1 THEN f.[Hours] ELSE NULL END),
		[TotalHr] = SUM(f.[Hours])
	FROM
		_TimeToFinish f
	GROUP BY
		f.[ReaderId]
),
_FastSpPg1 AS (
	SELECT
		ttf.*,
		[Row] = ROW_NUMBER() OVER(PARTITION BY ttf.[ReaderId] ORDER BY ttf.[SpeedPg] DESC)
	FROM _TimeToFinish ttf
),
_FastSpPg AS (
	SELECT f.*
	FROM _FastSpPg1 f
	WHERE f.[Row] = 1
),
_FastSpHr1 AS (
	SELECT
		ttf.*,
		[Row] = ROW_NUMBER() OVER(PARTITION BY ttf.[ReaderId] ORDER BY ttf.[SpeedHr] DESC)
	FROM _TimeToFinish ttf
),
_FastSpHr AS (
	SELECT f.*
	FROM _FastSpHr1 f
	WHERE f.[Row] = 1
),
_SlowSpPg1 AS (
	SELECT
		ttf.*,
		[Row] = ROW_NUMBER() OVER(PARTITION BY ttf.[ReaderId] ORDER BY ttf.[SpeedPg])
	FROM _TimeToFinish ttf
	WHERE ttf.[SpeedPg] IS NOT NULL
),
_SlowSpPg AS (
	SELECT f.*
	FROM _SlowSpPg1 f
	WHERE f.[Row] = 1
),
_SlowSpHr1 AS (
	SELECT
		ttf.*,
		[Row] = ROW_NUMBER() OVER(PARTITION BY ttf.[ReaderId] ORDER BY ttf.[SpeedHr])
	FROM _TimeToFinish ttf
	WHERE ttf.[SpeedHr] IS NOT NULL
),
_SlowSpHr AS (
	SELECT f.*
	FROM _SlowSpHr1 f
	WHERE f.[Row] = 1
),
_OldestBook1 AS (
	SELECT
		ttf.*,
		[Row] = ROW_NUMBER() OVER(PARTITION BY ttf.[ReaderId] ORDER BY ttf.[Published])
	FROM _TimeToFinish ttf
	WHERE ttf.[Published] IS NOT NULL
),
_OldestBook AS (
	SELECT f.*
	FROM _OldestBook1 f
	WHERE f.[Row] = 1
),
_NewestBook1 AS (
	SELECT
		ttf.*,
		[Row] = ROW_NUMBER() OVER(PARTITION BY ttf.[ReaderId] ORDER BY ttf.[Published] DESC)
	FROM _TimeToFinish ttf
	WHERE ttf.[Published] IS NOT NULL
),
_NewestBook AS (
	SELECT f.*
	FROM _NewestBook1 f
	WHERE f.[Row] = 1
),
_LongBook1 AS (
	SELECT
		ttf.*,
		[Row] = ROW_NUMBER() OVER(PARTITION BY ttf.[ReaderId] ORDER BY ttf.[Pages] DESC)
	FROM _TimeToFinish ttf
	WHERE ttf.[Pages] IS NOT NULL
),
_LongBook AS (
	SELECT f.*
	FROM _LongBook1 f
	WHERE f.[Row] = 1
),
_ShortBook1 AS (
	SELECT
		ttf.*,
		[Row] = ROW_NUMBER() OVER(PARTITION BY ttf.[ReaderId] ORDER BY ttf.[Pages])
	FROM _TimeToFinish ttf
	WHERE ttf.[Pages] IS NOT NULL
),
_ShortBook AS (
	SELECT f.*
	FROM _ShortBook1 f
	WHERE f.[Row] = 1
)
SELECT
	t.*,
	[FastPgBookId] = fp.[BookId],
	[FastPgTimeToFinish] = fp.[TimeToFinish],
	[FastPgSpeedPg] = fp.[SpeedPg],
	[FastPgSpeedHr] = fp.[SpeedHr],
	[FastHrBookId] = fh.[BookId],
	[FastHrTimeToFinish] = fh.[TimeToFinish],
	[FastHrSpeedPg] = fh.[SpeedPg],
	[FastHrSpeedHr] = fh.[SpeedHr],
	[SlowPgBookId] = sp.[BookId],
	[SlowPgTimeToFinish] = sp.[TimeToFinish],
	[SlowPgSpeedPg] = sp.[SpeedPg],
	[SlowPgSpeedHr] = sp.[SpeedHr],
	[SlowHrBookId] = sh.[BookId],
	[SlowHrTimeToFinish] = sh.[TimeToFinish],
	[SlowHrSpeedPg] = sh.[SpeedPg],
	[SlowHrSpeedHr] = sh.[SpeedHr],
	[NewBookId] = nb.[BookId],
	[OldBookId] = ob.[BookId],
	[LongBookId] = lb.[BookId],
	[ShortBookId] = sb.[BookId]
FROM
	_TTFFvNF2 t INNER JOIN
	_FastSpPg fp ON t.[ReaderId] = fp.[ReaderId] INNER JOIN
	_FastSpHr fh ON t.[ReaderId] = fh.[ReaderId] INNER JOIN
	_SlowSpPg sp ON t.[ReaderId] = sp.[ReaderId] INNER JOIN
	_SlowSpHr sh ON t.[ReaderId] = sh.[ReaderId] INNER JOIN
	_OldestBook ob ON t.[ReaderId] = ob.[ReaderId] INNER JOIN
	_NewestBook nb ON t.[ReaderId] = nb.[ReaderId] INNER JOIN
	_LongBook lb ON t.[ReaderId] = lb.[ReaderId] INNER JOIN
	_ShortBook sb ON t.[ReaderId] = sb.[ReaderId]
";

			migrationBuilder.Sql(sql);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW [dbo].[ReaderStats]");
        }
    }
}
