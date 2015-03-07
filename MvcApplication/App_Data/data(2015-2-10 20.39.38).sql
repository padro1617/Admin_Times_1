--http://demo.douco.com/admin 
-- DATE : 2015-03-03 20:50:42
-- SQL Server 2014
-- 网站后台管理系统数据库
--时代视觉工作室
--Times Visual Studio

--USE [master]
--GO

--/****** Object:  Database [Admin_Times_1]    Script Date: 2015/2/6 8:21:31 ******/
--DROP DATABASE [Admin_Times_1]
--GO

--/****** Object:  Database [Admin_Times_1]    Script Date: 2015/2/6 8:21:31 ******/
--CREATE DATABASE [Admin_Times_1]
-- CONTAINMENT = NONE
-- ON  PRIMARY 
--( NAME = N'Admin_Times_1', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\Admin_Times_1.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
-- LOG ON 
--( NAME = N'Admin_Times_1_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\Admin_Times_1_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
--GO

--ALTER DATABASE [Admin_Times_1] SET COMPATIBILITY_LEVEL = 120
--GO

--IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
--begin
--EXEC [Admin_Times_1].[dbo].[sp_fulltext_database] @action = 'enable'
--end
--GO

--ALTER DATABASE [Admin_Times_1] SET ANSI_NULL_DEFAULT OFF 
--GO

--ALTER DATABASE [Admin_Times_1] SET ANSI_NULLS OFF 
--GO

--ALTER DATABASE [Admin_Times_1] SET ANSI_PADDING OFF 
--GO

--ALTER DATABASE [Admin_Times_1] SET ANSI_WARNINGS OFF 
--GO

--ALTER DATABASE [Admin_Times_1] SET ARITHABORT OFF 
--GO

--ALTER DATABASE [Admin_Times_1] SET AUTO_CLOSE OFF 
--GO

--ALTER DATABASE [Admin_Times_1] SET AUTO_SHRINK OFF 
--GO

--ALTER DATABASE [Admin_Times_1] SET AUTO_UPDATE_STATISTICS ON 
--GO

--ALTER DATABASE [Admin_Times_1] SET CURSOR_CLOSE_ON_COMMIT OFF 
--GO

--ALTER DATABASE [Admin_Times_1] SET CURSOR_DEFAULT  GLOBAL 
--GO

--ALTER DATABASE [Admin_Times_1] SET CONCAT_NULL_YIELDS_NULL OFF 
--GO

--ALTER DATABASE [Admin_Times_1] SET NUMERIC_ROUNDABORT OFF 
--GO

--ALTER DATABASE [Admin_Times_1] SET QUOTED_IDENTIFIER OFF 
--GO

--ALTER DATABASE [Admin_Times_1] SET RECURSIVE_TRIGGERS OFF 
--GO

--ALTER DATABASE [Admin_Times_1] SET  DISABLE_BROKER 
--GO

--ALTER DATABASE [Admin_Times_1] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
--GO

--ALTER DATABASE [Admin_Times_1] SET DATE_CORRELATION_OPTIMIZATION OFF 
--GO

--ALTER DATABASE [Admin_Times_1] SET TRUSTWORTHY OFF 
--GO

--ALTER DATABASE [Admin_Times_1] SET ALLOW_SNAPSHOT_ISOLATION OFF 
--GO

--ALTER DATABASE [Admin_Times_1] SET PARAMETERIZATION SIMPLE 
--GO

--ALTER DATABASE [Admin_Times_1] SET READ_COMMITTED_SNAPSHOT OFF 
--GO

--ALTER DATABASE [Admin_Times_1] SET HONOR_BROKER_PRIORITY OFF 
--GO

--ALTER DATABASE [Admin_Times_1] SET RECOVERY FULL 
--GO

--ALTER DATABASE [Admin_Times_1] SET  MULTI_USER 
--GO

--ALTER DATABASE [Admin_Times_1] SET PAGE_VERIFY CHECKSUM  
--GO

--ALTER DATABASE [Admin_Times_1] SET DB_CHAINING OFF 
--GO

--ALTER DATABASE [Admin_Times_1] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
--GO

--ALTER DATABASE [Admin_Times_1] SET TARGET_RECOVERY_TIME = 0 SECONDS 
--GO

--ALTER DATABASE [Admin_Times_1] SET DELAYED_DURABILITY = DISABLED 
--GO

--ALTER DATABASE [Admin_Times_1] SET  READ_WRITE 
--GO



--还需建立索引
USE Admin_Times_1
go
if(NOT EXISTS(select TOP 1 id from sys.sysobjects where id=OBJECT_ID('times_admin')))  
--网站管理员列表
	CREATE TABLE times_admin (
	  user_id TINYINT NOT NULL PRIMARY KEY IDENTITY(1,1),
	  user_name varchar(60) NOT NULL default '',
	  email varchar(60) NOT NULL default '',
	  password varchar(32) NOT NULL default '',
	  last_ip varchar(15) NOT NULL default '',
	  add_time DATETIME NOT NULL DEFAULT GETDATE(),
	  login_time DATETIME NOT NULL DEFAULT GETDATE()
	) 

--INSERT INTO times_admin VALUES('1','admin','admin@admin.com','7fef6171469e80d32c0559f88b377245','ALL','1377768032','1411098722','127.0.0.1');

if(NOT EXISTS(select TOP 1 id from sys.sysobjects where id=OBJECT_ID('times_admin_log')))  
	CREATE TABLE times_admin_log (
	  id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	  user_id SMALLINT NOT NULL,
	  action varchar(255) NOT NULL default '',
	  ip varchar(15) NOT NULL default '',
	  create_time DATETIME NOT NULL DEFAULT GETDATE()
	)

if(NOT EXISTS(select TOP 1 id from sys.sysobjects where id=OBJECT_ID('times_config')))  
	CREATE TABLE times_config (
	  id smallint NOT NULL IDENTITY(1,1),
	  name varchar(80) NOT NULL PRIMARY KEY ,
	  value NVARCHAR(4000) NOT NULL default '',--网站底部显示 html或js 等配置
	  remark NVARCHAR(500) NOT NULL default '',--描述说明
	  --type varchar(10) NOT NULL default '',
	  --box varchar(255) NOT NULL default '',
	  --tab varchar(10) NOT NULL default 'main',
	  sort tinyint NOT NULL default '50'
	)

--自定义单页
if(NOT EXISTS(select TOP 1 id from sys.sysobjects where id=OBJECT_ID('times_page'))) 
	CREATE TABLE times_page (
	  id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	  unique_id nvarchar(30) NOT NULL default '',--别名
	  parent_id smallint NOT NULL default '0',
	  image varchar(255) NOT NULL default '',--缩略图路径
	  page_name nvarchar(150) NOT NULL default '',
	  keywords nvarchar(255) NOT NULL default '',
	  description nvarchar(255) NOT NULL default ''
	)

if(NOT EXISTS(select TOP 1 id from sys.sysobjects where id=OBJECT_ID('times_article')))  
	CREATE TABLE times_article (
	  id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	  cat_id smallint NOT NULL,
	  title nvarchar(150) NOT NULL default '',
	  image varchar(255) NOT NULL default '',--缩略图路径
	  click smallint NOT NULL default 0,
	  description nvarchar(1000) NOT NULL default '',
	  home_sort TINYINT NOT NULL default 0,--首页显示
	  isshow BIT NOT NULL DEFAULT 1,--是否显示
	  add_time  DATETIME NOT NULL DEFAULT GETDATE(),
	  edit_time  DATETIME NOT NULL DEFAULT GETDATE()
	)
--产品页面
if(NOT EXISTS(select TOP 1 id from sys.sysobjects where id=OBJECT_ID('times_product'))) 
	CREATE TABLE times_product (
	  id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	  cat_id smallint NOT NULL,
	  product_name varchar(150) NOT NULL default '',
	  price decimal(10,2) NOT NULL default '0.00',
	  size NVARCHAR(255) NOT NULL default '',--尺寸
	  years NVARCHAR(255) NOT NULL default '',--年代
	  number NVARCHAR(255) NOT NULL default '',--产品编号
	  image nvarchar(255) NOT NULL default '',--缩略图路径
	  description nvarchar(1000) NOT NULL default '',--简介描述
	  link_url varchar(1000) NOT NULL default '',--产品链接页面 可能链接到淘宝页面
	  home_sort TINYINT NOT NULL default 0,--首页显示
	  isshow BIT NOT NULL DEFAULT 1,--是否显示
	  add_time  DATETIME NOT NULL DEFAULT GETDATE(),
	  edit_time  DATETIME NOT NULL DEFAULT GETDATE()
	)


--详情描述
if(NOT EXISTS(select TOP 1 id from sys.sysobjects where id=OBJECT_ID('times_content'))) 
BEGIN
	CREATE TABLE times_content (
	  id INT NOT NULL IDENTITY(1,1),
	  dataid int NOT NULL ,--产品或资讯的详细内容
	  type TINYINT NOT NULL,--类别区分来自产品还是资讯  1:代表产品 2:代表新闻 10:单页内容
	  content NVARCHAR(MAX) NOT NULL DEFAULT '',
	  add_time  DATETIME NOT NULL DEFAULT GETDATE()
	)
	alter table times_content add primary key(dataid,type)
END
    
--关键词 标签云 表
if(NOT EXISTS(select TOP 1 id from sys.sysobjects where id=OBJECT_ID('times_keywords'))) 
	CREATE TABLE times_keywords (
	  id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	  keywords nvarchar(255) NOT NULL,
	  sort INT NOT NULL default 0,--排序
	)
--关键词 标签云 关联关系表
if(NOT EXISTS(select TOP 1 id from sys.sysobjects where id=OBJECT_ID('times_keywords_relative'))) 
BEGIN
	CREATE TABLE times_keywords_relative (
	  id INT NOT NULL IDENTITY(1,1),
	  dataid INT NOT NULL,
	  key_id INT NOT NULL,
	  type TINYINT NOT NULL,--类别区分来自产品还是资讯  1:代表产品 2:代表新闻
	  add_time  DATETIME NOT NULL DEFAULT GETDATE()
	)
	alter table times_keywords_relative add primary key(dataid,key_id,type)
END
--详情图片区域
if(NOT EXISTS(select TOP 1 id from sys.sysobjects where id=OBJECT_ID('times_pic'))) 
BEGIN
	CREATE TABLE times_pic (
	  id INT NOT NULL IDENTITY(1,1),
	  dataid int NOT NULL ,
	  type TINYINT NOT NULL,--目录类型 1:代表产品 2:代表新闻 10:单页内容
	  name nvarchar(255) NOT NULL default '',
	  isDefault bit NOT NULL default 0,--是否为默认图 第一张
	  sort TINYINT NOT NULL default 0,--排序
	  add_time  DATETIME NOT NULL DEFAULT GETDATE()
	) 
	alter table times_pic add primary key(dataid,type)
END
    

	--类别区域
if(NOT EXISTS(select TOP 1 id from sys.sysobjects where id=OBJECT_ID('times_category')))  
	CREATE TABLE times_article_category (
	  cat_id smallint NOT NULL PRIMARY KEY IDENTITY(1,1),
	  unique_id nvarchar(30) NOT NULL default '',--别名
	  type TINYINT NOT NULL,--目录类型 1:代表产品 2:代表新闻 10:单页内容
	  cat_name nvarchar(255) NOT NULL default '',
	  keywords nvarchar(255) NOT NULL default '',
	  description nvarchar(255) NOT NULL default '',
	  parent_id smallint NOT NULL default '0',
	  sort smallint NOT NULL default 50,
	  add_time  DATETIME NOT NULL DEFAULT GETDATE()
	) 


	
--INSERT INTO times_link VALUES('1','豆壳网络','http://www.douco.com','127');
--导航区
if(NOT EXISTS(select TOP 1 id from sys.sysobjects where id=OBJECT_ID('times_nav'))) 
CREATE TABLE times_nav (
  id smallint NOT NULL PRIMARY KEY IDENTITY(1,1),
  nav_name nvarchar(255) NOT NULL,
  module varchar(20) NOT NULL,--来自哪里
  dataid int NOT NULL DEFAULT 0,
  parent_id smallint NOT NULL default '0',
  selflink  varchar(1000) NOT NULL default '0',--自定义导航连接
  type TINYINT NOT NULL,--1：顶部 2：主导航 3：底部
  sort smallint NOT NULL default '50',
  add_time DATETIME NOT NULL DEFAULT GETDATE()
) 
--INSERT INTO times_article_category VALUES('1','company','公司动态','公司动态','公司的最新新闻在此发布','0','10');
--INSERT INTO times_article_category VALUES('2','industry','行业新闻','行业新闻','最新行业资讯','0','20');

--留言板
if(NOT EXISTS(select TOP 1 id from sys.sysobjects where id=OBJECT_ID('times_guestbook'))) 
	CREATE TABLE times_guestbook (
	  id  INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	  title nvarchar(150) NOT NULL default '',
	  name nvarchar(60) NOT NULL default '',
	  contact nvarchar(150) NOT NULL default '',
	  content NVARCHAR(MAX) NOT NULL,
	  isread bit NOT NULL default 0,
	  ip varchar(15) NOT NULL default '',
	  add_time DATETIME NOT NULL DEFAULT GETDATE()
	)

----友情链接
--if(NOT EXISTS(select TOP 1 id from sys.sysobjects where id=OBJECT_ID('times_link'))) 
--	CREATE TABLE times_link (
--	  id smallint NOT NULL PRIMARY KEY IDENTITY(1,1),
--	  link_name nvarchar(60) NOT NULL default '',
--	  link_url varchar(255) NOT NULL default '',
--	  sort smallint NOT NULL default '50',
--	)
	

--INSERT INTO times_admin_log VALUES('1','1381887740','1','系统设置: 编辑成功','127.0.0.1');
--INSERT INTO times_admin_log VALUES('2','1381887745','1','编辑导航: 公司简介','127.0.0.1');
--INSERT INTO times_admin_log VALUES('3','1381887749','1','编辑幻灯: 广告图片01','127.0.0.1');
--INSERT INTO times_admin_log VALUES('4','1381887753','1','编辑单页面: 联系我们','127.0.0.1');
--INSERT INTO times_admin_log VALUES('5','1381887756','1','编辑商品分类: 电子数码','127.0.0.1');
--INSERT INTO times_admin_log VALUES('6','1381887759','1','编辑文章分类: 公司动态','127.0.0.1');
--INSERT INTO times_admin_log VALUES('7','1381887775','1','编辑单页面: 企业荣誉','127.0.0.1');
--INSERT INTO times_admin_log VALUES('8','1381887778','1','编辑单页面: 公司简介','127.0.0.1');
--INSERT INTO times_admin_log VALUES('9','1381887781','1','编辑单页面: 营销网络','127.0.0.1');
--INSERT INTO times_admin_log VALUES('10','1381980020','1','恢复备份: douphp.sql','127.0.0.1');
--INSERT INTO times_admin_log VALUES('11','1382062229','1','管理员登录: 登录成功！','127.0.0.1');
--INSERT INTO times_admin_log VALUES('12','1382103121','1','管理员登录: 登录成功！','127.0.0.1');
--INSERT INTO times_admin_log VALUES('13','1399999423','1','恢复备份: douphp.sql','127.0.0.1');
--INSERT INTO times_admin_log VALUES('14','1399999438','1','添加导航: 留言板','127.0.0.1');
--INSERT INTO times_admin_log VALUES('15','1399999494','1','删除导航: 联系我们','127.0.0.1');
--INSERT INTO times_admin_log VALUES('16','1400078869','1','管理员登录: 登录成功！','127.0.0.1');

	
--INSERT INTO times_config VALUES('site_name','DouPHP轻量级企业网站管理系统','NVARCHAR(MAX)','','main','1');
--INSERT INTO times_config VALUES('site_title','DouPHP轻量级企业网站管理系统','NVARCHAR(MAX)','','main','2');
--INSERT INTO times_config VALUES('site_keywords','DouPHP,轻量级企业网站管理系统','NVARCHAR(MAX)','','main','3');
--INSERT INTO times_config VALUES('site_description','DouPHP,轻量级企业网站管理系统','NVARCHAR(MAX)','','main','4');
--INSERT INTO times_config VALUES('site_theme','default','select','','main','5');
--INSERT INTO times_config VALUES('site_logo','logo.gif','file','','main','6');
--INSERT INTO times_config VALUES('site_address','福建省漳州市芗城区','NVARCHAR(MAX)','','main','7');
--INSERT INTO times_config VALUES('site_closed','0','radio','','main','8');
--INSERT INTO times_config VALUES('icp','','NVARCHAR(MAX)','','main','9');
--INSERT INTO times_config VALUES('tel','0596-8888888','NVARCHAR(MAX)','','main','10');
--INSERT INTO times_config VALUES('fax','0596-6666666','NVARCHAR(MAX)','','main','11');
--INSERT INTO times_config VALUES('qq','','NVARCHAR(MAX)','','main','12');
--INSERT INTO times_config VALUES('email','your@domain.com','NVARCHAR(MAX)','','main','13');
--INSERT INTO times_config VALUES('language','zh_cn','select','','main','14');
--INSERT INTO times_config VALUES('rewrite','1','radio','','main','15');
--INSERT INTO times_config VALUES('sitemap','1','radio','','main','16');
--INSERT INTO times_config VALUES('captcha','1','radio','','main','17');
--INSERT INTO times_config VALUES('guestbook_check_chinese','1','radio','','main','18');
--INSERT INTO times_config VALUES('code','','textarea','','main','19');
--INSERT INTO times_config VALUES('display_product','10','NVARCHAR(MAX)','','display','1');
--INSERT INTO times_config VALUES('display_article','10','NVARCHAR(MAX)','','display','2');
--INSERT INTO times_config VALUES('display_guestbook','10','NVARCHAR(MAX)','','display','3');
--INSERT INTO times_config VALUES('home_display_product','4','NVARCHAR(MAX)','','display','4');
--INSERT INTO times_config VALUES('home_display_article','5','NVARCHAR(MAX)','','display','5');
--INSERT INTO times_config VALUES('thumb_width','135','NVARCHAR(MAX)','','display','6');
--INSERT INTO times_config VALUES('thumb_height','135','NVARCHAR(MAX)','','display','7');
--INSERT INTO times_config VALUES('price_decimal','2','NVARCHAR(MAX)','','display','8');
--INSERT INTO times_config VALUES('defined_product','','NVARCHAR(MAX)','','defined','1');
--INSERT INTO times_config VALUES('defined_article','','NVARCHAR(MAX)','','defined','2');
--INSERT INTO times_config VALUES('mobile_name','DouPHP','NVARCHAR(MAX)','','mobile','1');
--INSERT INTO times_config VALUES('mobile_title','DouPHP触屏版','NVARCHAR(MAX)','','mobile','2');
--INSERT INTO times_config VALUES('mobile_keywords','DouPHP,DouPHP触屏版','NVARCHAR(MAX)','','mobile','3');
--INSERT INTO times_config VALUES('mobile_description','DouPHP,DouPHP触屏版','NVARCHAR(MAX)','','mobile','4');
--INSERT INTO times_config VALUES('mobile_theme','default','select','','mobile','5');
--INSERT INTO times_config VALUES('mobile_logo','','file','','mobile','6');
--INSERT INTO times_config VALUES('mobile_display_product','10','NVARCHAR(MAX)','','mobile','7');
--INSERT INTO times_config VALUES('mobile_display_article','10','NVARCHAR(MAX)','','mobile','8');
--INSERT INTO times_config VALUES('mobile_display_guestbook','10','NVARCHAR(MAX)','','mobile','9');
--INSERT INTO times_config VALUES('mobile_home_display_product','6','NVARCHAR(MAX)','','mobile','10');
--INSERT INTO times_config VALUES('mobile_home_display_article','6','NVARCHAR(MAX)','','mobile','11');
--INSERT INTO times_config VALUES('build_date','1377768032','hidden','','','100');
--INSERT INTO times_config VALUES('hash_code','166d0de32dafdef9ab26e10130dd115b','hidden','','','101');
--INSERT INTO times_config VALUES('douphp_version','v1.2 Release 20141130','hidden','','','102');

--INSERT INTO times_nav VALUES('1','page','公司简介','1','0','middle','10');
--INSERT INTO times_nav VALUES('2','page','企业荣誉','2','1','middle','10');
--INSERT INTO times_nav VALUES('3','page','发展历程','3','1','middle','20');
--INSERT INTO times_nav VALUES('4','page','联系我们','4','1','middle','30');
--INSERT INTO times_nav VALUES('5','product_category','产品中心','0','0','middle','20');
--INSERT INTO times_nav VALUES('6','article_category','文章中心','0','0','middle','30');
--INSERT INTO times_nav VALUES('7','page','营销网络','6','0','middle','40');
--INSERT INTO times_nav VALUES('8','page','人才招聘','5','0','middle','60');
--INSERT INTO times_nav VALUES('9','page','联系我们','4','0','middle','70');
--INSERT INTO times_nav VALUES('10','product_category','电子数码','1','5','middle','10');
--INSERT INTO times_nav VALUES('11','product_category','家居百货','2','5','middle','20');
--INSERT INTO times_nav VALUES('12','product_category','母婴用品','3','5','middle','30');
--INSERT INTO times_nav VALUES('13','article_category','公司动态','1','6','middle','10');
--INSERT INTO times_nav VALUES('14','article_category','行业新闻','2','6','middle','20');
--INSERT INTO times_nav VALUES('15','page','企业荣誉','2','0','middle','50');
--INSERT INTO times_nav VALUES('17','page','公司简介','1','0','bottom','10');
--INSERT INTO times_nav VALUES('18','page','营销网络','6','0','bottom','20');
--INSERT INTO times_nav VALUES('19','page','企业荣誉','2','0','bottom','30');
--INSERT INTO times_nav VALUES('20','page','人才招聘','5','0','bottom','40');
--INSERT INTO times_nav VALUES('21','page','联系我们','4','0','bottom','50');
--INSERT INTO times_nav VALUES('22','product_category','智能手机','4','10','middle','1');
--INSERT INTO times_nav VALUES('23','product_category','平板电脑','5','10','middle','2');
--INSERT INTO times_nav VALUES('24','guestbook','留言反馈','0','0','top','20');
--INSERT INTO times_nav VALUES('25','page','公司简介','1','0','mobile','10');
--INSERT INTO times_nav VALUES('26','product_category','产品中心','0','0','mobile','20');
--INSERT INTO times_nav VALUES('27','article_category','文章中心','0','0','mobile','30');
--INSERT INTO times_nav VALUES('28','page','企业荣誉','2','0','mobile','40');
--INSERT INTO times_nav VALUES('29','page','营销网络','6','0','mobile','50');
--INSERT INTO times_nav VALUES('30','page','人才招聘','5','0','mobile','60');
--INSERT INTO times_nav VALUES('31','page','联系我们','4','0','mobile','70');
--INSERT INTO times_nav VALUES('32','guestbook','留言反馈','0','0','mobile','80');
--INSERT INTO times_nav VALUES('33','mobile','手机版','0','0','top','10');
--INSERT INTO times_nav VALUES('34','mobile','手机版','0','0','bottom','60');


--INSERT INTO times_page VALUES('1','about','0','公司简介','DouPHP 是一款轻量级企业网站管理系统，基于PHP+Mysql架构的，可运行在Linux、Windows、MacOSX、Solaris等各种平台上，系统搭载Smarty模板引擎，支持自定义伪静态，前台模板采用DIV+CSS设计，后台界面设计简洁明了，功能简单易具有良好的用户体验，稳定性好、扩展性及安全性强，可面向中小型站点提供网站建设解决方案。','公司简介','公司简介');
--INSERT INTO times_page VALUES('2','honor','1','企业荣誉','企业荣誉','企业荣誉','企业荣誉');
--INSERT INTO times_page VALUES('3','history','1','发展历程','发展历程','发展历程','发展历程');
--INSERT INTO times_page VALUES('4','contact','1','联系我们','通讯地址：<br />\r\n<span style=\"color:#D7D7D7;\">--------------------------------------------------------------------------------------------------------------------------------</span><br />\r\n福建省漳州市芗城区，邮编363000<br />\r\n<br />\r\n客服邮箱：<br />\r\n<span style=\"color:#D7D7D7;\">--------------------------------------------------------------------------------------------------------------------------------</span><br />\r\nDouPHP售后服务邮箱：email@email.com<br />\r\nDouPHP业务受理邮箱：<span>email@email.com</span><br />\r\n如您需要订制开发请在邮件中注明您的大概要求，我们将在一个工作日内给予回复。<br />\r\n<br />\r\n客服电话：<br />\r\n<span style=\"color:#D7D7D7;\">--------------------------------------------------------------------------------------------------------------------------------</span><br />\r\n<span>DouPHP</span>的建站咨询电话为 0596-1234567。<br />\r\n客服电话工作时间为周一至周日 8:00-20:00，节假日不休息，免长途话费。<br />\r\n我们将随时为您献上真诚的服务。<br />\r\n<br />\r\n网站网址：<br />\r\n<span style=\"color:#D7D7D7;\">--------------------------------------------------------------------------------------------------------------------------------</span><br />\r\nwww.douco.com<br />','联系我们','联系我们');
--INSERT INTO times_page VALUES('5','job','0','人才招聘','人才招聘','人才招聘','人才招聘');
--INSERT INTO times_page VALUES('6','market','0','营销网络','营销网络','营销网络','营销网络');

--INSERT INTO times_product_category VALUES('1','digital','电子数码','ipad,iphone,三星','电子产品销售','0','10');
--INSERT INTO times_product_category VALUES('2','home','家居百货','家居用品,沙发桌椅,生活周边','家居百货产品销售','0','20');
--INSERT INTO times_product_category VALUES('3','baby','母婴用品','奶粉,营养辅食,尿裤湿巾,喂养用品,洗护用','母婴用品销售','0','30');
--INSERT INTO times_product_category VALUES('4','phone','智能手机','iphone,blackberry','智能手机销售','1','50');
--INSERT INTO times_product_category VALUES('5','tabletpc','平板电脑','ipad','平板电脑销售','1','50');

----广告区域
--if(NOT EXISTS(select TOP 1 id from sys.sysobjects where id=OBJECT_ID('times_show'))) 
--	CREATE TABLE times_show (
--	  id smallint(5) unsigned NOT NULL auto_increment,
--	  show_name varchar(60) NOT NULL default '',
--	  show_link varchar(255) NOT NULL default '',
--	  show_img varchar(255) NOT NULL,
--	  type varchar(10) NOT NULL,
--	  sort tinyint(1) unsigned NOT NULL default '50'
--	) 

--INSERT INTO times_show VALUES('1','广告图片01','http://www.douco.com','data/slide/20130514acunau.jpg','pc','1');
--INSERT INTO times_show VALUES('2','广告图片02','http://www.douco.com','data/slide/20130514rjzqdt.jpg','pc','2');
--INSERT INTO times_show VALUES('3','广告图片03','http://www.douco.com','data/slide/20130514xxsctt.jpg','pc','3');
--INSERT INTO times_show VALUES('4','广告图片04','http://www.douco.com','data/slide/20130523hiqafl.jpg','pc','4');
--INSERT INTO times_show VALUES('5','手机版广告图片01','http://m.douco.com','data/slide/m/20140921rqmzcp.jpg','mobile','10');
--INSERT INTO times_show VALUES('6','手机版广告图片02','http://m.douco.com','data/slide/m/20140921kwoypm.jpg','mobile','20');
--INSERT INTO times_show VALUES('7','手机版广告图片03','http://m.douco.com','data/slide/m/20140921ypmnew.jpg','mobile','30');
--INSERT INTO times_show VALUES('8','手机版广告图片04','http://m.douco.com','data/slide/m/20140921demloy.jpg','mobile','40');

