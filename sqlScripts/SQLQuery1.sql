select * from TimeSheetDetails

insert into TimeSheetDetails values ('126','test task','2019-04-17 23:17:18.960','23:17:18.9600000',null,'0',1,1,null)


INSERT INTO TimeSheetDetails values('12','some task','2019-04-17 23:39:18','23:39:18','','1','0','0','')

--drop table TimeSheetDetails



select * from TimeSheetDetails where TaskStartTime > '11:00'

UPDATE TimeSheetDetails SET TaskID='1213', TaskName='task-1232', TaskEndTime='13:15:31.9303306', IsCoded=0, IsReviewed=1, IsCheckin=0, Comment=NULL WHERE ID=1


SELECT * FROM TimeSheetDetails

 WHERE TaskEndTime IS NULL ORDER BY TaskStartTime DESC

UPDATE TimeSheetDetails SET TaskEndTime='13:25:12.6', IsCoded=1 where ID=9


UPDATE TimeSheetDetails SET , TaskID=121, TaskName=asdf-121, TaskEndTime=13:03:52.6066383, IsCoded=0, IsReviewed=0, IsCheckin=0, Comment=NULL

update TimeSheetDetails set Comment=null where ID=1
select * from TimeSheetDetails where id=1




1	1213	task-1232	2019-04-18 13:14:57.257	13:14:57.2740572	13:15:31.9303306	0	1	0	NULL


insert into ClientDetails values('C1',null)
insert into ClientDetails values('C2',null)
insert into ClientDetails values('C4',null)


select * from ClientDetails
select * from TimeSheetDetails

SELECT * FROM TimeSheetDetails WHERE TaskDate >= '2019-04-18' AND TaskDate <= '2019-04-19'

insert into ClientDetails set

update ClientDetails set ClientName='a' where ClientName='a'

drop table ClientDetails
drop table TimeSheetDetails



