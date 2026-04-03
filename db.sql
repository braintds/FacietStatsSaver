-- =========================
-- Создание базы данных
-- =========================

-- Подключение к базе (в psql)
-- \c game_stats;


-- =========================
-- TABLE: Account
-- =========================
CREATE TABLE Account (
    AccountId SERIAL PRIMARY KEY,
    AccountName VARCHAR(100) NOT NULL UNIQUE,
    CreatedAt TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);


-- =========================
-- TABLE: GameSession
-- =========================
CREATE TABLE GameSession (
    SessionId SERIAL PRIMARY KEY,
    AccountId INT NOT NULL,

    IntervalStartDate TIMESTAMP NOT NULL,
    IntervalEndDate TIMESTAMP NOT NULL,

    SessionStartDate TIMESTAMP NOT NULL,
    SessionEndDate TIMESTAMP,

    CONSTRAINT fk_session_account
        FOREIGN KEY (AccountId)
        REFERENCES Account(AccountId)
        ON DELETE CASCADE,

    CONSTRAINT chk_interval_valid
        CHECK (IntervalEndDate >= IntervalStartDate),

    CONSTRAINT chk_session_valid
        CHECK (SessionEndDate IS NULL 
               OR SessionEndDate >= SessionStartDate)
);

-- Индекс для быстрых выборок сессий по аккаунту
CREATE INDEX idx_session_account 
ON GameSession(AccountId);



-- =========================
-- TABLE: Match
-- =========================
CREATE TABLE Match (
    MatchId SERIAL PRIMARY KEY,
    SessionId INT NOT NULL,

    MapName VARCHAR(100) NOT NULL,
    PlayedAt TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,

    Kills INT NOT NULL DEFAULT 0 CHECK (Kills >= 0),
    Assists INT NOT NULL DEFAULT 0 CHECK (Assists >= 0),
    Deaths INT NOT NULL DEFAULT 0 CHECK (Deaths >= 0),

    DoubleKills INT NOT NULL DEFAULT 0 CHECK (DoubleKills >= 0),
    TripleKills INT NOT NULL DEFAULT 0 CHECK (TripleKills >= 0),
    QuadroKills INT NOT NULL DEFAULT 0 CHECK (QuadroKills >= 0),

    MVPs INT NOT NULL DEFAULT 0 CHECK (MVPs >= 0),

    IsWin BOOLEAN NOT NULL,

    CONSTRAINT fk_match_session
        FOREIGN KEY (SessionId)
        REFERENCES GameSession(SessionId)
        ON DELETE CASCADE
);

-- Индекс для быстрых выборок матчей по сессии
CREATE INDEX idx_match_session 
ON Match(SessionId);

-- Индекс для аналитики по дате
CREATE INDEX idx_match_playedat 
ON Match(PlayedAt);