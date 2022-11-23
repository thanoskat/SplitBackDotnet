BEGIN TRANSACTION;
CREATE TABLE IF NOT EXISTS "Sessions" (
	"SessionId"	INTEGER NOT NULL,
	"RefreshToken"	TEXT NOT NULL,
	"UserId"	INTEGER NOT NULL,
	"Unique"	TEXT,
	CONSTRAINT "PK_Sessions" PRIMARY KEY("SessionId" AUTOINCREMENT),
	CONSTRAINT "FK_Sessions_Users_UserId" FOREIGN KEY("UserId") REFERENCES "Users"("UserId") ON DELETE CASCADE
);
CREATE TABLE IF NOT EXISTS "Labels" (
	"LabelId"	INTEGER NOT NULL,
	"Name"	TEXT NOT NULL,
	"GroupId"	INTEGER,
	CONSTRAINT "PK_Labels" PRIMARY KEY("LabelId" AUTOINCREMENT),
	CONSTRAINT "FK_Labels_Groups_GroupId" FOREIGN KEY("GroupId") REFERENCES "Groups"("GroupId")
);
CREATE TABLE IF NOT EXISTS "Users" (
	"UserId"	INTEGER NOT NULL,
	"Email"	TEXT NOT NULL,
	"Nickname"	TEXT NOT NULL,
	CONSTRAINT "PK_Users" PRIMARY KEY("UserId" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "Groups" (
	"GroupId"	INTEGER NOT NULL,
	"CreatorUserId"	INTEGER NOT NULL,
	"Title"	TEXT NOT NULL,
	CONSTRAINT "FK_Groups_Users_CreatorUserId" FOREIGN KEY("CreatorUserId") REFERENCES "Users"("UserId") ON DELETE CASCADE,
	CONSTRAINT "PK_Groups" PRIMARY KEY("GroupId" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "GroupUser" (
	"GroupsGroupId"	INTEGER NOT NULL,
	"MembersUserId"	INTEGER NOT NULL,
	CONSTRAINT "FK_GroupUser_Groups_GroupsGroupId" FOREIGN KEY("GroupsGroupId") REFERENCES "Groups"("GroupId") ON DELETE CASCADE,
	CONSTRAINT "FK_GroupUser_Users_MembersUserId" FOREIGN KEY("MembersUserId") REFERENCES "Users"("UserId") ON DELETE CASCADE,
	CONSTRAINT "PK_GroupUser" PRIMARY KEY("GroupsGroupId","MembersUserId")
);
CREATE TABLE IF NOT EXISTS "Transfers" (
	"TransferId"	INTEGER NOT NULL,
	"Amount"	TEXT NOT NULL,
	"Description"	TEXT,
	"GroupId"	INTEGER,
	"ReceiverUserId"	INTEGER NOT NULL,
	"SenderUserId"	INTEGER NOT NULL,
	CONSTRAINT "FK_Transfers_Users_ReceiverUserId" FOREIGN KEY("ReceiverUserId") REFERENCES "Users"("UserId") ON DELETE CASCADE,
	CONSTRAINT "FK_Transfers_Users_SenderUserId" FOREIGN KEY("SenderUserId") REFERENCES "Users"("UserId") ON DELETE CASCADE,
	CONSTRAINT "FK_Transfers_Groups_GroupId" FOREIGN KEY("GroupId") REFERENCES "Groups"("GroupId"),
	CONSTRAINT "PK_Transfers" PRIMARY KEY("TransferId" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "ExpenseUsers" (
	"ExpenseId"	INTEGER NOT NULL,
	"SpenderId"	INTEGER NOT NULL,
	"SpenderAmount"	TEXT NOT NULL,
	CONSTRAINT "PK_ExpenseUsers" PRIMARY KEY("ExpenseId","SpenderId"),
	CONSTRAINT "FK_ExpenseUsers_Expenses_ExpenseId" FOREIGN KEY("ExpenseId") REFERENCES "Expenses"("ExpenseId") ON DELETE CASCADE,
	CONSTRAINT "FK_ExpenseUsers_Users_SpenderId" FOREIGN KEY("SpenderId") REFERENCES "Users"("UserId") ON DELETE CASCADE
);
CREATE TABLE IF NOT EXISTS "Expenses" (
	"ExpenseId"	INTEGER NOT NULL,
	"Amount"	TEXT NOT NULL,
	"Description"	TEXT NOT NULL,
	"GroupId"	INTEGER,
	"LabelId"	INTEGER,
	CONSTRAINT "PK_Expenses" PRIMARY KEY("ExpenseId" AUTOINCREMENT),
	CONSTRAINT "FK_Expenses_Groups_GroupId" FOREIGN KEY("GroupId") REFERENCES "Groups"("GroupId"),
	CONSTRAINT "FK_Expenses_Labels_LabelId" FOREIGN KEY("LabelId") REFERENCES "Labels"("LabelId")
);
CREATE TABLE IF NOT EXISTS "ExpenseParticipants" (
	"ParticipantId"	INTEGER NOT NULL,
	"ExpenseId"	INTEGER NOT NULL,
	"ContributionAmount"	TEXT NOT NULL,
	CONSTRAINT "FK_ExpenseParticipants_Users_ParticipantId" FOREIGN KEY("ParticipantId") REFERENCES "Users"("UserId") ON DELETE CASCADE,
	CONSTRAINT "FK_ExpenseParticipants_Expenses_ExpenseId" FOREIGN KEY("ExpenseId") REFERENCES "Expenses"("ExpenseId") ON DELETE CASCADE,
	CONSTRAINT "PK_ExpenseParticipants" PRIMARY KEY("ExpenseId","ParticipantId")
);
CREATE TABLE IF NOT EXISTS "PendingTransaction" (
	"Id"	INTEGER NOT NULL,
	"UserId"	INTEGER NOT NULL,
	"SenderId"	INTEGER NOT NULL,
	"Amount"	TEXT NOT NULL,
	"GroupId"	INTEGER,
	CONSTRAINT "FK_PendingTransaction_Groups_GroupId" FOREIGN KEY("GroupId") REFERENCES "Groups"("GroupId"),
	CONSTRAINT "PK_PendingTransaction" PRIMARY KEY("Id" AUTOINCREMENT)
);
INSERT INTO "Sessions" VALUES (1,'ed14671a-a270-4681-ab51-a91d809e196b',1,'29e2096a-80b2-4fb6-9076-955311690f0e');
INSERT INTO "Sessions" VALUES (2,'b554aae6-1eb9-4f7e-973a-d761837e5af8',1,'45ac56e0-cab9-49f7-93ef-82d906fbdf40');
INSERT INTO "Sessions" VALUES (3,'2b709c1e-0fb3-4d38-b44e-7d2ae30416e1',2,'7bc99115-eb8e-4cdf-a811-8791a0bc57c0');
INSERT INTO "Labels" VALUES (2,'string',NULL);
INSERT INTO "Users" VALUES (1,'dima@gmail.com','dima');
INSERT INTO "Users" VALUES (2,'kristi@gmail.com','kristi');
INSERT INTO "Groups" VALUES (1,1,'XDDD');
INSERT INTO "GroupUser" VALUES (1,1);
INSERT INTO "GroupUser" VALUES (1,2);
INSERT INTO "ExpenseUsers" VALUES (2,1,'3.0');
INSERT INTO "ExpenseUsers" VALUES (2,2,'17.0');
INSERT INTO "Expenses" VALUES (2,'15.0','aaa',1,2);
INSERT INTO "ExpenseParticipants" VALUES (1,2,'7.0');
INSERT INTO "ExpenseParticipants" VALUES (2,2,'8.0');
CREATE INDEX IF NOT EXISTS "IX_Labels_GroupId" ON "Labels" (
	"GroupId"
);
CREATE INDEX IF NOT EXISTS "IX_Sessions_UserId" ON "Sessions" (
	"UserId"
);
CREATE UNIQUE INDEX IF NOT EXISTS "IX_Users_Email" ON "Users" (
	"Email"
);
CREATE INDEX IF NOT EXISTS "IX_Groups_CreatorUserId" ON "Groups" (
	"CreatorUserId"
);
CREATE INDEX IF NOT EXISTS "IX_GroupUser_MembersUserId" ON "GroupUser" (
	"MembersUserId"
);
CREATE INDEX IF NOT EXISTS "IX_Transfers_GroupId" ON "Transfers" (
	"GroupId"
);
CREATE INDEX IF NOT EXISTS "IX_Transfers_ReceiverUserId" ON "Transfers" (
	"ReceiverUserId"
);
CREATE INDEX IF NOT EXISTS "IX_Transfers_SenderUserId" ON "Transfers" (
	"SenderUserId"
);
CREATE INDEX IF NOT EXISTS "IX_ExpenseUsers_SpenderId" ON "ExpenseUsers" (
	"SpenderId"
);
CREATE INDEX IF NOT EXISTS "IX_Expenses_GroupId" ON "Expenses" (
	"GroupId"
);
CREATE INDEX IF NOT EXISTS "IX_Expenses_LabelId" ON "Expenses" (
	"LabelId"
);
CREATE INDEX IF NOT EXISTS "IX_ExpenseParticipants_ParticipantId" ON "ExpenseParticipants" (
	"ParticipantId"
);
CREATE INDEX IF NOT EXISTS "IX_PendingTransaction_GroupId" ON "PendingTransaction" (
	"GroupId"
);
COMMIT;
