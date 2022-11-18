BEGIN TRANSACTION;
CREATE TABLE IF NOT EXISTS "Groups" (
	"Id"	INTEGER NOT NULL,
	"Title"	TEXT NOT NULL,
	"CreatorId"	INTEGER NOT NULL,
	CONSTRAINT "FK_Groups_Users_CreatorId" FOREIGN KEY("CreatorId") REFERENCES "Users"("Id") ON DELETE CASCADE,
	CONSTRAINT "PK_Groups" PRIMARY KEY("Id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "Sessions" (
	"Id"	INTEGER NOT NULL,
	"RefreshToken"	TEXT NOT NULL,
	"UserId"	INTEGER NOT NULL,
	"Unique"	TEXT,
	CONSTRAINT "FK_Sessions_Users_UserId" FOREIGN KEY("UserId") REFERENCES "Users"("Id") ON DELETE CASCADE,
	CONSTRAINT "PK_Sessions" PRIMARY KEY("Id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "GroupUser" (
	"GroupsId"	INTEGER NOT NULL,
	"MembersId"	INTEGER NOT NULL,
	CONSTRAINT "FK_GroupUser_Groups_GroupsId" FOREIGN KEY("GroupsId") REFERENCES "Groups"("Id") ON DELETE CASCADE,
	CONSTRAINT "FK_GroupUser_Users_MembersId" FOREIGN KEY("MembersId") REFERENCES "Users"("Id") ON DELETE CASCADE,
	CONSTRAINT "PK_GroupUser" PRIMARY KEY("GroupsId","MembersId")
);
CREATE TABLE IF NOT EXISTS "Labels" (
	"Id"	INTEGER NOT NULL,
	"Name"	TEXT NOT NULL,
	"GroupId"	INTEGER,
	CONSTRAINT "FK_Labels_Groups_GroupId" FOREIGN KEY("GroupId") REFERENCES "Groups"("Id"),
	CONSTRAINT "PK_Labels" PRIMARY KEY("Id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "Shares" (
	"Id"	INTEGER NOT NULL,
	"UserId"	INTEGER NOT NULL,
	"Amount"	TEXT NOT NULL,
	"ExpenseId"	INTEGER,
	CONSTRAINT "FK_Shares_Users_UserId" FOREIGN KEY("UserId") REFERENCES "Users"("Id") ON DELETE CASCADE,
	CONSTRAINT "FK_Shares_Expenses_ExpenseId" FOREIGN KEY("ExpenseId") REFERENCES "Expenses"("Id"),
	CONSTRAINT "PK_Shares" PRIMARY KEY("Id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "Expenses" (
	"Id"	INTEGER NOT NULL,
	"Amount"	TEXT NOT NULL,
	"Description"	TEXT NOT NULL,
	"GroupId"	INTEGER,
	"LabelId"	INTEGER,
	CONSTRAINT "FK_Expenses_Groups_GroupId" FOREIGN KEY("GroupId") REFERENCES "Groups"("Id"),
	CONSTRAINT "FK_Expenses_Labels_LabelId" FOREIGN KEY("LabelId") REFERENCES "Labels"("Id"),
	CONSTRAINT "PK_Expenses" PRIMARY KEY("Id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "Transfers" (
	"Id"	INTEGER NOT NULL,
	"Amount"	TEXT NOT NULL,
	"Description"	TEXT,
	"GroupId"	INTEGER,
	"ReceiverId"	INTEGER NOT NULL,
	"SenderId"	INTEGER NOT NULL,
	CONSTRAINT "FK_Transfers_Groups_GroupId" FOREIGN KEY("GroupId") REFERENCES "Groups"("Id"),
	CONSTRAINT "FK_Transfers_Users_ReceiverId" FOREIGN KEY("ReceiverId") REFERENCES "Users"("Id") ON DELETE CASCADE,
	CONSTRAINT "FK_Transfers_Users_SenderId" FOREIGN KEY("SenderId") REFERENCES "Users"("Id") ON DELETE CASCADE,
	CONSTRAINT "PK_Transfers" PRIMARY KEY("Id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "Users" (
	"Id"	INTEGER NOT NULL,
	"Email"	TEXT NOT NULL,
	"Nickname"	TEXT NOT NULL,
	CONSTRAINT "PK_Users" PRIMARY KEY("Id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "ExpenseUser" (
	"ExpenseId"	INTEGER NOT NULL,
	"SpenderId"	INTEGER NOT NULL,
	"SpenderAmount"	TEXT NOT NULL,
	CONSTRAINT "FK_ExpenseUser_Expenses_ExpenseId" FOREIGN KEY("ExpenseId") REFERENCES "Expenses"("Id") ON DELETE CASCADE,
	CONSTRAINT "FK_ExpenseUser_Users_SpenderId" FOREIGN KEY("SpenderId") REFERENCES "Users"("Id") ON DELETE CASCADE,
	CONSTRAINT "PK_ExpenseUser" PRIMARY KEY("ExpenseId","SpenderId")
);
CREATE INDEX IF NOT EXISTS "IX_Groups_CreatorId" ON "Groups" (
	"CreatorId"
);
CREATE INDEX IF NOT EXISTS "IX_GroupUser_MembersId" ON "GroupUser" (
	"MembersId"
);
CREATE INDEX IF NOT EXISTS "IX_Labels_GroupId" ON "Labels" (
	"GroupId"
);
CREATE INDEX IF NOT EXISTS "IX_Sessions_UserId" ON "Sessions" (
	"UserId"
);
CREATE INDEX IF NOT EXISTS "IX_Shares_ExpenseId" ON "Shares" (
	"ExpenseId"
);
CREATE INDEX IF NOT EXISTS "IX_Shares_UserId" ON "Shares" (
	"UserId"
);
CREATE INDEX IF NOT EXISTS "IX_Expenses_GroupId" ON "Expenses" (
	"GroupId"
);
CREATE INDEX IF NOT EXISTS "IX_Expenses_LabelId" ON "Expenses" (
	"LabelId"
);
CREATE INDEX IF NOT EXISTS "IX_Transfers_GroupId" ON "Transfers" (
	"GroupId"
);
CREATE INDEX IF NOT EXISTS "IX_Transfers_ReceiverId" ON "Transfers" (
	"ReceiverId"
);
CREATE INDEX IF NOT EXISTS "IX_Transfers_SenderId" ON "Transfers" (
	"SenderId"
);
CREATE UNIQUE INDEX IF NOT EXISTS "IX_Users_Email" ON "Users" (
	"Email"
);
CREATE INDEX IF NOT EXISTS "IX_ExpenseUser_SpenderId" ON "ExpenseUser" (
	"SpenderId"
);
COMMIT;
