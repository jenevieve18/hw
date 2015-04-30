use healthwatch
go

alter table ExerciseLang add ExerciseContent text
go

select * from WiseLang

SELECT TOP 1 wl.Wise, wl.WiseBy, w.WiseID FROM WiseLang wl INNER JOIN Wise w ON wl.WiseID = w.WiseID WHERE wl.LangID = 1 ORDER BY w.LastShown ASC
