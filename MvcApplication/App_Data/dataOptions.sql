--http://demo.douco.com/admin 
USE Admin_Times_1
go
--================
--lpd 20150229
--��ѯ��վ����Ա��Ա�б���Ϣ
--================
CREATE PROCEDURE timesadmin_select
AS
	SELECT * FROM dbo.times_admin
GO

--================
--lpd 20150229
--��ѯ��վ����Ա��Ա��Ϣ
--================
CREATE PROCEDURE timesadmin_getinfo(
	@user_id TINYINT
)
AS
	SELECT * FROM dbo.times_admin WHERE user_id=@user_id
GO

--================
--lpd 20150229
--��¼��鲢�ҷ��ض�Ӧ��¼
--================
CREATE PROCEDURE timesadmin_logincheck(
	@user_name VARCHAR(60),
	@password VARCHAR(32),
	@result INT OUTPUT
)
AS
	IF(EXISTS(SELECT TOP 1 user_id FROM dbo.times_admin WHERE user_name=@user_name AND password=@password) )
	BEGIN
		set @result=1
		SELECT TOP 1 * FROM dbo.times_admin WHERE user_name=@user_name AND password=@password
	END
	ELSE
	BEGIN
		set @result=0
	END
GO

--================
--lpd 20150229
--����һ����¼��վ����Ա��Ա��Ϣ
--@result -2 �Ѵ�����ͬ���Ƶ���Ϣ
--================
CREATE PROCEDURE timesadmin_insert(
	@user_name VARCHAR(60),
	@password VARCHAR(32),
	@email  varchar(60),
	@last_ip  varchar(15),
	@result INT OUTPUT
)
AS
	IF(EXISTS(SELECT TOP 1 user_id FROM dbo.times_admin WHERE user_name=@user_name))
	BEGIN
		SET @result=-2
	END
	ELSE
	BEGIN
		INSERT dbo.times_admin
		( user_name ,
		  email ,
		  password ,
		  last_ip ,
		  add_time
		)
		VALUES  ( @user_name , -- user_name - varchar(60)
				  @email , -- email - varchar(60)
				  @password , -- password - varchar(32)
				  @last_ip , -- last_ip - varchar(15)
				  GETDATE()  -- add_time - datetime
				)
		SET @result=SCOPE_IDENTITY()
	END
GO

--================
--lpd 20150229
--�༭һ����¼��վ����Ա��Ա��Ϣ
--@result -2 �Ѵ�����ͬ���Ƶ���Ϣ
--================
CREATE PROCEDURE timesadmin_update(
	@user_id TINYINT,
	@user_name VARCHAR(60),
	@password VARCHAR(32),
	@email  varchar(60),
	@last_ip  varchar(15),
	@result INT OUTPUT
)
AS
	IF(EXISTS(SELECT TOP 1 user_id FROM dbo.times_admin WHERE user_name=@user_name))
	BEGIN
		SET @result=-2
	END
	ELSE
	BEGIN
		UPDATE dbo.times_admin SET
		user_name=CASE WHEN @user_name IS NULL THEN user_name ELSE @user_name END
		,password=CASE WHEN @password IS NULL THEN password ELSE @password END
		,email=CASE WHEN @email IS NULL THEN email ELSE @email END
		,last_ip=CASE WHEN @last_ip IS NULL THEN last_ip ELSE @last_ip END
		WHERE user_id=@user_id
		
		SET @result=@@ROWCOUNT
	END
GO

--================
--lpd 20150229
--ɾ��һ����¼��վ����Ա��Ա��Ϣ
--@result -2 �Ѵ�����ͬ���Ƶ���Ϣ
--================
CREATE PROCEDURE timesadmin_delete(
	@user_id TINYINT,
	@result INT OUTPUT
)
AS
	IF(@user_id IS NULL)
		SET @result=0
	DELETE dbo.times_admin WHERE user_id=@user_id
	--��û�з�������ʱ����0
	IF(@@ERROR=0)
		SET @result=1
	ELSE		
		SET @result=0
GO
