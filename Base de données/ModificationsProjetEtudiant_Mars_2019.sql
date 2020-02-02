USE [CreditSante]
GO

INSERT INTO [dbo].[modificationAffichage]
           ([id]
           ,[titre]
           ,[contenu])
     VALUES
           ('Statistique',
           ''
           ,'1/1/1')
GO

USE [CreditSante]
GO

/****** Object:  Table [dbo].[evenement]    Script Date: 20/03/2019 09:17:42 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[evenement](
	[id] [nvarchar](36) NOT NULL,
	[titre] [nvarchar](100) NOT NULL,
	[date_publication] [datetime] NOT NULL,
	[contenu] [nvarchar](3000) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

USE [CreditSante]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Onglets](
	[id] [int] NOT NULL,
	[titre] [nvarchar](50) NULL,
	[valid] [bit] NULL,
	) 
GO

INSERT INTO [dbo].[Onglets]
           ([id]
           ,[titre]
           ,[valid])
     VALUES
           ('1','Capsules Santés','1'),
		   ('2','Statistiques','1'),
           ('3','Historique','1'),
		   ('4','Photos','1'),
		   ('5','Nous joindre','1'),
		   ('6','Evenements','1')
GO

