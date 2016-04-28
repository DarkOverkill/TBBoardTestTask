create database DataFolder
use DataFolder
create table Folders
(
	Id int not null identity(1,1),
	Name nvarchar(256),
	[Level] int,
	ParentId int

	constraint pk_folders_id primary key(Id)
)

CREATE TABLE Files (
	Id int not null identity(1,1),
	ParentFolderId int not null,
	[FileName] nvarchar(256) COLLATE Cyrillic_General_CI_AS,
	[DocumentFile] varbinary(max)

	constraint pk_files_id primary key (Id),
	constraint fk_files_parentFolderId_folders_id
	foreign key (ParentFolderId) references Folders(Id)
);


insert into Files([FileName], [DocumentFile]) 
select ('ring.wav') as FileName, * 
from openrowset(bulk 'D:\ring.wav', single_blob) as DocumentFile
 
insert into Files(ParentFolderId, [FileName], [DocumentFile]) VALUES (2, 'ring.wav', 'D:\ring.wav')

select * from Files
select * from Folders
drop table Folders
drop table Files