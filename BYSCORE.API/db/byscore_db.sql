-- MySQL dump 10.13  Distrib 8.0.3-rc, for macos10.12 (x86_64)
--
-- Host: 10.192.1.60    Database: beyondsoft_resume
-- ------------------------------------------------------
-- Server version   8.0.15

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

CREATE Database byscore_db;
Use byscore_db;

--
-- Table structure for table `applicationlog`
--

DROP TABLE IF EXISTS `applicationlog`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `applicationlog` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `application` varchar(50) DEFAULT NULL,
  `logged` datetime DEFAULT NULL,
  `level` varchar(50) DEFAULT NULL,
  `message` varchar(4000) DEFAULT NULL,
  `logger` varchar(250) DEFAULT NULL,
  `callsite` varchar(512) DEFAULT NULL,
  `exception` text,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;


--
-- Table structure for table `config_info`
--

DROP TABLE IF EXISTS `config_info`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `config_info` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT '序号',
  `code` varchar(128) DEFAULT NULL COMMENT '编码',
  `type` int(11) DEFAULT NULL COMMENT '类型',
  `name` varchar(128) DEFAULT NULL COMMENT '名称',
  `remarks` varchar(1024) DEFAULT NULL COMMENT '备注',
  `parent_id` int(11) DEFAULT NULL COMMENT '父级id',
  `created_time` datetime DEFAULT NULL COMMENT '创建时间',
  `is_delete` tinyint(1) DEFAULT NULL COMMENT '是否删除',
  `sort_number` int(11) DEFAULT NULL COMMENT '排序号',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='配置信息表 ';
/*!40101 SET character_set_client = @saved_cs_client */;

LOCK TABLES `config_info` WRITE;
INSERT INTO byscore_db.config_info (id, code, type, name, remarks, parent_id, created_time, is_delete, sort_number) VALUES (1, 'c594e3105eff405b8c9a63a5b6454041', 1, '测试部门', '测试部门', 0, '2019-04-13 23:24:16', 0, 0);
INSERT INTO byscore_db.config_info (id, code, type, name, remarks, parent_id, created_time, is_delete, sort_number) VALUES (2, 'd8fa46c0421143a493501f27ae1c0d24', 2, '深圳', '深圳', 0, '2019-04-13 23:24:56', 0, 0);
UNLOCK TABLES;
--
-- Table structure for table `menu`
--

DROP TABLE IF EXISTS `menu`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `menu` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT '菜单id',
  `level` int(11) DEFAULT NULL,
  `parent_id` int(11) DEFAULT NULL,
  `code` varchar(32) DEFAULT NULL COMMENT '菜单编码',
  `c_name` varchar(128) DEFAULT NULL COMMENT '菜单名称',
  `e_name` varchar(128) DEFAULT NULL,
  `url` varchar(128) DEFAULT NULL COMMENT '菜单地址',
  `type` int(11) DEFAULT NULL COMMENT '类型 类型：0导航菜单；1操作按钮。',
  `icon` varchar(32) DEFAULT NULL COMMENT '菜单图标',
  `remarks` varchar(1024) DEFAULT NULL COMMENT '菜单备注',
  `sort` int(11) DEFAULT NULL,
  `is_delete` tinyint(1) DEFAULT NULL COMMENT '是否删除',
  `created_time` datetime DEFAULT NULL COMMENT '创建时间',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='菜单表 ';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `menu`
--

LOCK TABLES `menu` WRITE;
/*!40000 ALTER TABLE `menu` DISABLE KEYS */;
INSERT INTO `menu` VALUES (1,0,0,'0bcd123a7db14cba8b27d647948c9467','首页','home','/home/index',0,'fa-home','首页',1,0,'2019-02-25 09:51:15')
                          ,(2,0,0,'0c4ba43276bb4bd69fe20cac01152d29','系统设置','sys_settiing','javascript:;',0,'fa-cog','系统设置',4,0,'2019-02-25 17:56:11')
                          ,(3,0,2,'3c8e91da927248568450eedec5788f04','菜单管理','sys_menu','/sys/menuset',0,'fa-bars','系统设置',3,0,'2019-02-25 17:56:11')
                          ,(4,0,3,'57c064fd0bd5423c99ecad016235dbfb','获取菜单','sys_getmenulist','/sys/getmenulist',2,NULL,'系统设置',4,0,'2019-02-25 17:56:11')
                          ,(5,0,3,'74d9b24ba5a543e28f98d98c2547389b','添加菜单','sys_menuadd','/sys/menuadd',2,NULL,'添加菜单',5,0,'2019-02-27 14:38:36')
                          ,(6,0,3,'b0c10ff05a7445c7b019dcc963a3c65b','编辑菜单','sys_menuedit','/sys/menuedit',2,NULL,'编辑菜单',6,0,'2019-02-27 14:45:38')
                          ,(7,0,3,'e091f1be21794e229a15b6e450b9c449','获取菜单详细','sys_GetMenuByCode','/sys/getmenubycode',2,NULL,'根据code获取菜单详细信息',7,0,'2019-02-27 14:49:32')
                          ,(8,0,2,'aa9efa2bfcc2415c9b9aef8d7be3729c','角色管理','sys_Role','/sys/rolemanage',0,'fa-users','角色管理',4,0,'2019-02-27 15:29:22')
                          ,(9,0,8,'3d2cf6f3cbce4c8a9164a040634b8298','添加角色','sys_roleadd','/sys/roleadd',1,NULL,'添加角色',0,0,'2019-02-27 15:54:48')
                          ,(10,0,8,'369c4b5c0a9747bdb77ad81878d23b06','添加角色接口','sys_roleadd','/sys/roleadd',2,NULL,'添加角色接口',0,0,'2019-02-27 15:56:35')
                          ,(11,0,8,'a37648196e794f3c88715cc4b0214802','获取角色列表','sys_getRoleList','/sys/getrolelist',2,NULL,NULL,0,0,'2019-02-28 14:25:07')
                          ,(12,0,8,'5092b09831dc4bfd954f2bfde1110a6a','编辑角色','sys_roleEdit','/sys/roleedit',1,NULL,NULL,0,0,'2019-02-28 14:50:14')
                          ,(13,0,8,'9ff7632e0c094818af8084857a01fe8b','编辑角色接口','sys_roleedit','/sys/roleedit',2,NULL,NULL,0,0,'2019-02-28 14:51:05')
                          ,(14,0,8,'bdcd862109914f26a18ea26dd202b308','删除角色','sys_roledel','/sys/delrole',2,NULL,NULL,0,0,'2019-02-28 14:52:16')
                          ,(15,0,3,'4686de53c2ed419b9915c22187714258','删除菜单','sys_menudel','/sys/delmenu',2,NULL,NULL,0,0,'2019-02-28 14:53:49')
                          ,(16,0,2,'e7c4668794ba44f391f5d765c4f6afb2','用户管理','sys_usermanage','/sys/usermanage',0,'fa-user',NULL,3,0,'2019-02-28 16:00:44')
                          ,(17,0,2,'c45d146f8d9f441d856e8d69efb979c8','配置管理','sys_configinfo','/configinfo/index',0,'fa-book',NULL,8,0,'2019-03-05 14:16:17')
                          ,(18,0,2,'bda84a59d7784ea5bd0b154d68efeb09','个人中心','sys_userinfo','/home/userinfo',1,'fa-address-card-o',NULL,2,0,'2019-03-06 14:22:04'),(19,0,16,'403421d4b08e4212a6ddf78a9a55d3a8','获取用户列表','sys_GetUserList','/sys/getuserlist',2,NULL,NULL,6,0,'2019-03-07 10:40:14')
                          ,(20,0,16,'a87d3c990913451d99082ef8fa643666','添加用户','sys_useradd','/sys/useradd',1,NULL,NULL,7,0,'2019-03-07 10:41:27')
                          ,(21,0,16,'3fa3bd9ab6434b37a8715cf452e5995e','添加用户','sys_useradd','/sys/useradd',2,NULL,NULL,7,0,'2019-03-07 10:42:15')
                          ,(22,0,16,'693cddd32230424fbc81571ffd427772','编辑用户','sys_useredit','/sys/useredit',1,NULL,NULL,6,0,'2019-03-07 10:43:06')
                          ,(23,0,16,'22958e6fac61453c85a209388ba90915','编辑用户','sys_useredit','/sys/useredit',2,NULL,NULL,5,0,'2019-03-07 10:43:45')
                          ,(24,0,16,'a39048cce0024178bc86431ec7be2aff','删除用户','sys_deluser','/sys/deluser',1,NULL,NULL,7,0,'2019-03-07 10:44:40')
                          ,(25,0,16,'75569673628c47be8a1bd6b40fed2a77','冻结用户','sys_freezeuser','/sys/freezeuser',1,NULL,NULL,6,0,'2019-03-07 10:47:41')
                          ,(26,0,16,'0cdce005761a47c0ba9913c2a973415a','修改密码','sys_upuserpwd','/sys/upuserpwd',1,NULL,NULL,3,0,'2019-03-07 10:48:27')
                          ,(27,0,16,'1886ff24aae94422b970417546e47a7c','获取部门列表','sys_getdepartments','/configinfo/getdepartments',2,NULL,NULL,0,0,'2019-03-07 10:51:26')
                          ,(28,0,17,'d4efb12794dc4801b8f604aad5a88d15','区域管理','sys_areamanage','/configinfo/areamanage',1,NULL,NULL,0,0,'2019-03-07 10:52:28')
                          ,(29,0,17,'90b5d7bac4054442888a3e4dfd1265f2','获取区域列表','sys_getareas','/configinfo/getareas',2,NULL,NULL,0,0,'2019-03-07 10:53:16')
                          ,(30,0,17,'d7e5c7807fa34372a98efe13781a44d6','根据code获取配置','sys_getbycode','/configinfo/getbycode',2,NULL,NULL,0,0,'2019-03-07 10:57:44')
                          ,(31,0,17,'f4bd40270d7f4a68818aa209041b5515','添加配置','sys_configinfoadd','/configinfo/configinfoadd',2,NULL,NULL,0,0,'2019-03-07 10:58:08')
                          ,(32,0,17,'02c9577f5cd34a8da8d9085b62dafc6b','编辑配置','sys_configinfoedit','/configinfo/configinfoedit',2,NULL,NULL,0,0,'2019-03-07 10:58:32')
                          ,(33,0,17,'47ff4fd586244189809092d19118da5e','删除配置','sys_configinfodel','/configinfo/configinfodel',2,NULL,NULL,0,0,'2019-03-07 10:59:02')
                          ,(34,0,17,'09cc8039a0b343f59e21475a1689a5e1','上传头像','sys_upload','/sys/upload',2,NULL,NULL,0,0,'2019-03-07 14:25:06')
                          ,(35,0,2,'9dd33b73d8e244ef814fe0efa0f8ea62','日志','sys_log','/applog/index',0,'fa-book',NULL,8,0,'2019-03-07 16:39:24')
                          ,(36,0,35,'83c48a80d41e4752abb0f56fa9552d59','获取日志数据','sys_getapploglist','/applog/getapploglist',2,'fa-book',NULL,8,0,'2019-03-07 16:39:24');
/*!40000 ALTER TABLE `menu` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `position`
--


--
-- Table structure for table `resume`
--



DROP TABLE IF EXISTS `role`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `role` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT '角色id',
  `code` varchar(32) DEFAULT NULL COMMENT '角色code',
  `c_name` varchar(32) DEFAULT NULL COMMENT '角色名',
  `e_name` varchar(32) DEFAULT NULL,
  `description` varchar(1024) DEFAULT NULL COMMENT '角色说明',
  `created_time` datetime DEFAULT NULL COMMENT '创建时间',
  `is_delete` tinyint(1) DEFAULT NULL COMMENT '是否删除',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='角色表 ';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `role`
--

LOCK TABLES `role` WRITE;
/*!40000 ALTER TABLE `role` DISABLE KEYS */;
INSERT INTO `role` VALUES (1,'c1006460281142089033477b89fcd7b1','超级管理员','admin','拥有最高权限','2019-02-22 10:15:13',0);
/*!40000 ALTER TABLE `role` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `role_menu`
--

DROP TABLE IF EXISTS `role_menu`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `role_menu` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `code` varchar(32) DEFAULT NULL,
  `role_id` int(11) DEFAULT NULL COMMENT '角色id',
  `menu_id` int(11) DEFAULT NULL COMMENT '菜单Code',
  `created_time` datetime DEFAULT NULL COMMENT '创建时间',
  `is_delete` tinyint(1) DEFAULT NULL COMMENT '是否删除',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='角色权限表 ';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `role_menu`
--

LOCK TABLES `role_menu` WRITE;
/*!40000 ALTER TABLE `role_menu` DISABLE KEYS */;
INSERT INTO `role_menu` VALUES
(1,'c41047860d914b1abdac6a93e0f46d78',1,1,'2019-03-25 14:25:34',0)
,(2,'2cb119a5b2cc4b52bde3a1f6c11b2151',1,2,'2019-03-25 14:25:34',0)
,(3,'3064b0775e5a4b88945351c4b7bd93fa',1,3,'2019-03-25 14:25:34',0)
,(4,'6fdbbc9435bc474f831a14fa64126bc8',1,4,'2019-03-25 14:25:34',0)
,(5,'192d072d445c4f96af31992a6abf14e9',1,5,'2019-03-25 14:25:34',0)
,(6,'7bf2cf35d4ec46f1930369aa00caa948',1,6,'2019-03-25 14:25:34',0)
,(7,'febb230127f04789bd0a89be21d9032d',1,7,'2019-03-25 14:25:34',0)
,(8,'343e46793e1845e88da7691c16e5e902',1,8,'2019-03-25 14:25:34',0)
,(9,'cf7bb010864c4e40a33d8bfadc4f13b9',1,9,'2019-03-25 14:25:34',0)
,(10,'4cc03a3a04ac467ba3ec9a230200412a',1,10,'2019-03-25 14:25:34',0)
,(11,'966335cf20d14abab2e5736e8eae0f32',1,11,'2019-03-25 14:25:34',0)
,(12,'8b43ea1cc32b41a3976610a9cb06862a',1,12,'2019-03-25 14:25:34',0)
,(13,'c4f03c1609e34963ab6f4307470c9870',1,13,'2019-03-25 14:25:34',0)
,(14,'4ce6f610be7c4ba7b3a167be8cdefdf0',1,14,'2019-03-25 14:25:34',0)
,(15,'33a33076e2d6497a9a82620639706994',1,15,'2019-03-25 14:25:34',0)
,(16,'96b4f7d6a1824ccf8095b9a6f0418d79',1,16,'2019-03-25 14:25:34',0)
,(17,'8bded3434c094562bd2e7632f37314c1',1,17,'2019-03-25 14:25:34',0)
,(18,'30b8f481d2464d74b3668e3fd2e05c7b',1,18,'2019-03-25 14:25:34',0)
,(19,'0bbe532eb36a40d7b332198931c7bab2',1,19,'2019-03-25 14:25:34',0)
,(20,'10204e3ae8dd44a980824933f52074c6',1,20,'2019-03-25 14:25:34',0)
,(21,'7104af00db0c4cb1b6b6b9e842ef6e6e',1,21,'2019-03-25 14:25:34',0)
,(22,'d6179236deaf42e084d9fd08d8db3bde',1,22,'2019-03-25 14:25:34',0)
,(23,'4e88d54a72d043b683cc5265b46f85ad',1,23,'2019-03-25 14:25:34',0)
,(24,'aff7e18351b6407c9166ec629356dd16',1,24,'2019-03-25 14:25:34',0)
,(25,'b87f32ba92cb4d0f808131d6ec0712db',1,25,'2019-03-25 14:25:34',0)
,(26,'f1a4ea0a0ece4ccc862e60a087d019f0',1,26,'2019-03-25 14:25:34',0)
,(27,'a0e6fb99b0454dc4a52b5f5bbf4dc369',1,27,'2019-03-25 14:25:34',0)
,(28,'9c015bad29fb41b794774cda3ad2f77b',1,28,'2019-03-25 14:25:34',0)
,(29,'b2975bc5f5e84dd1adca8087e59bdfbf',1,29,'2019-03-25 14:25:34',0)
,(30,'3fd2adc6ab0a4c57bbfe6f980efb0b9e',1,30,'2019-03-25 14:25:34',0)
,(31,'e89d1865a483449ba8f8a82c157ecedb',1,31,'2019-03-25 14:25:34',0)
,(32,'ec946e5ca6d9404abf0b6b7c5c5b2f2b',1,32,'2019-03-25 14:25:34',0)
,(33,'2e9cfdfb139f4fa29c038b9a8a6e2ce4',1,33,'2019-03-25 14:25:34',0)
,(34,'3ee4f828e4f74fba9ebb01a7b0ebdae8',1,34,'2019-03-25 14:25:34',0)
,(35,'2bd036c5b1c94944893d93d6c7feec05',1,35,'2019-03-25 14:25:34',0)
,(36,'9bb82814d97243fd89b472dde0ae50b1',1,36,'2019-03-25 14:25:34',0);
/*!40000 ALTER TABLE `role_menu` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user`
--

DROP TABLE IF EXISTS `user`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `user` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT '用户id',
  `role_id` int(11) DEFAULT NULL,
  `department_id` int(11) DEFAULT NULL,
  `area_id` int(11) DEFAULT NULL,
  `code` varchar(32) DEFAULT NULL COMMENT '用户code',
  `user_number` varchar(128) DEFAULT NULL COMMENT '员工编号',
  `name` varchar(32) DEFAULT NULL COMMENT '姓名',
  `user_name` varchar(128) DEFAULT NULL COMMENT '用户名',
  `password` varchar(32) NOT NULL,
  `phone` varchar(32) DEFAULT NULL COMMENT '手机号',
  `email` varchar(128) DEFAULT NULL COMMENT '邮箱',
  `head_img` varchar(128) DEFAULT NULL COMMENT '头像',
  `is_freeze` tinyint(1) DEFAULT NULL COMMENT '是否冻结',
  `is_delete` tinyint(1) DEFAULT NULL COMMENT '是否删除',
  `created_time` datetime DEFAULT NULL COMMENT '创建时间',
  `last_login_time` datetime DEFAULT NULL COMMENT '最后登录时间',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='用户表 ';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user`
--

LOCK TABLES `user` WRITE;
/*!40000 ALTER TABLE `user` DISABLE KEYS */;
INSERT INTO `user` VALUES (1,1,1,2,'47ebf21d58f247dfb3d5d0951d284556','00000','admin','admin','e10adc3949ba59abbe56e057f20f883e','1111111111','aaa@163.com','/Upload/HeadImg/2019-04-14/eab5a27247e8887979801ec0e301f842.jpg',0,0,'2019-02-21 15:30:08','2019-03-29 17:58:41');
/*!40000 ALTER TABLE `user` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user_configinfo`
--



--
-- Table structure for table `user_menu`
--

DROP TABLE IF EXISTS `user_menu`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `user_menu` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键id',
  `code` varchar(32) DEFAULT NULL,
  `user_id` int(11) DEFAULT NULL COMMENT '用户code',
  `menu_id` int(11) DEFAULT NULL COMMENT '菜单code',
  `created_time` datetime DEFAULT NULL COMMENT '创建时间',
  `is_delete` tinyint(1) DEFAULT NULL COMMENT '是否删除',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='用户权限表 ';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user_menu`
--

LOCK TABLES `user_menu` WRITE;
/*!40000 ALTER TABLE `user_menu` DISABLE KEYS */;
INSERT INTO `user_menu` VALUES
  (1,'b91600e2091a470880f0fdafd9034aaf',1,1,'2019-03-29 17:55:31',0)
,(2,'6f05eeec9e0e4aa996984a3ad56500ec',1,2,'2019-03-29 17:55:31',0)
,(3,'31e1aa31f94242139a89472099b249b9',1,3,'2019-03-29 17:55:31',0)
,(4,'28b4e6e2818b4e33a815fb5514f59bab',1,4,'2019-03-29 17:55:31',0)
,(5,'847e5c7d548243e98436d64b643528ce',1,5,'2019-03-29 17:55:31',0)
,(6,'b1c948d8f9154a948b883db66ce90a83',1,6,'2019-03-29 17:55:31',0)
,(7,'4dc026ff026b4e3083099c43967423de',1,7,'2019-03-29 17:55:31',0)
,(8,'899ce30c34a64b20ad86c44d10c7a9a4',1,8,'2019-03-29 17:55:31',0)
,(9,'5dc663e9500f4399a2b02e8ce3b62c80',1,9,'2019-03-29 17:55:31',0)
,(10,'c198b9b3d51f422b9128dbfd2da30df9',1,10,'2019-03-29 17:55:31',0)
,(11,'b8142eddb1284b35856f5e0f8146d769',1,11,'2019-03-29 17:55:31',0)
,(12,'90cb19a3932f4f86b891da20564db23a',1,12,'2019-03-29 17:55:31',0)
,(13,'bc47f0909179490cb5271c4adcece4ad',1,13,'2019-03-29 17:55:31',0)
,(14,'b56d857346f44002a38b0334ad48901b',1,14,'2019-03-29 17:55:31',0)
,(15,'6d089c75f42e47d684f681c93452e37c',1,15,'2019-03-29 17:55:31',0)
,(16,'70415b3172d74b16becd1a16253fc5fe',1,16,'2019-03-29 17:55:31',0)
,(17,'0b156d0ff9fc4d10b29aa9836022b300',1,17,'2019-03-29 17:55:31',0)
,(18,'894f9177f1f44653b8ae7418ee512dce',1,18,'2019-03-29 17:55:31',0)
,(19,'8013f2ab395e4d51965186fa024814c4',1,19,'2019-03-29 17:55:31',0)
,(20,'f324da2a2b2f458e977fa3361c65cd2a',1,20,'2019-03-29 17:55:31',0)
,(21,'039705a5ef184be3a9b739a4e2b4f8e7',1,21,'2019-03-29 17:55:31',0)
,(22,'af9ae5b568214ed589ff901b1dfc141f',1,22,'2019-03-29 17:55:31',0)
,(23,'2f130433f8eb4c8fb046671efbda6307',1,23,'2019-03-29 17:55:31',0)
,(24,'cb85b3d690da42f7835df81983a5493b',1,24,'2019-03-29 17:55:31',0)
,(25,'2c6a4c35c6764401a4c176280d54dc1f',1,25,'2019-03-29 17:55:31',0)
,(26,'0d48196c120945a7a35f2a9cb4e83263',1,26,'2019-03-29 17:55:31',0)
,(27,'ef46d326af25477b9501bf3fb1edff34',1,27,'2019-03-29 17:55:31',0)
,(28,'d8624558781143f6817c3419e6b60490',1,28,'2019-03-29 17:55:31',0)
,(29,'49e93cc74e8343eeb9eabf5be15c8015',1,29,'2019-03-29 17:55:31',0)
,(30,'4e9b769f959c4456b722ac8285b0bd04',1,30,'2019-03-29 17:55:31',0)
,(31,'910fc3de5953439283710ecc65d49719',1,31,'2019-03-29 17:55:31',0)
,(32,'063cb7420ea34a91801522dabde1d9d3',1,32,'2019-03-29 17:55:31',0)
,(33,'d1cd5c4c491b4002b405e967fbf562d8',1,33,'2019-03-29 17:55:31',0)
,(34,'d770aa851d6842e5971b5ac4967df817',1,34,'2019-03-29 17:55:31',0)
,(35,'29e0257b8a674320addf8f21669216eb',1,35,'2019-03-29 17:55:31',0)
,(36,'b3b5cde7f282498dbe667854f7b94cff',1,36,'2019-03-29 17:55:31',0);

/*!40000 ALTER TABLE `user_menu` ENABLE KEYS */;
UNLOCK TABLES;


