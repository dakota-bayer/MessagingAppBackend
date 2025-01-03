CREATE EXTENSION IF NOT EXISTS "uuid-ossp";
       
CREATE TABLE IF NOT EXISTS message
(
    "Id"        UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    "Content"   TEXT NOT NULL,
    "CreatedAt" TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);