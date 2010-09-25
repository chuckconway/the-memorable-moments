CREATE TABLE [dbo].[Invitation] (
    [InvitationId] INT            IDENTITY (1, 1) NOT NULL,
    [Email]        NVARCHAR (250) NOT NULL,
    [UserId]       INT            NOT NULL,
    [CreateDate]   DATETIME2 (7)  NOT NULL,
    [Sent]         BIT            NOT NULL
);





