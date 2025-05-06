DROP TABLE IF EXISTS startup CASCADE;
DROP TABLE IF EXISTS investor CASCADE;
DROP TABLE IF EXISTS member CASCADE;
DROP TABLE IF EXISTS industry CASCADE;
DROP TABLE IF EXISTS investment_size CASCADE;
DROP TABLE IF EXISTS country CASCADE;

-- Country Table
CREATE TABLE country (
    country_id SERIAL PRIMARY KEY,
    country_name VARCHAR(50) NOT NULL
);

-- Industry Table (Creative Industry Categories)
CREATE TABLE industry (
    industry_id SERIAL PRIMARY KEY,
    industry_name VARCHAR(50) NOT NULL
);

-- Investment Size Table
CREATE TABLE investment_size (
    investment_size_id SERIAL PRIMARY KEY,
    investment_size_name VARCHAR(50) NOT NULL
);

-- Members Table (Stores basic member info for both startups and investors)
CREATE TABLE member (
    member_id SERIAL PRIMARY KEY,
    member_email VARCHAR(255) UNIQUE NOT NULL,
    member_type VARCHAR(10) NOT NULL CHECK (member_type IN ('Investor', 'Startup')), -- ENUM Alternative
    member_address VARCHAR(100) NOT NULL,
    member_phone VARCHAR(20) NOT NULL
);

-- Startup Table
CREATE TABLE startup (
    member_id INTEGER PRIMARY KEY REFERENCES member(member_id),
    name_startup VARCHAR(255) UNIQUE NOT NULL,
    overview_startup VARCHAR(600) UNIQUE NOT NULL, -- Retaining UNIQUE constraint
    country_id INTEGER NOT NULL REFERENCES country(country_id),
    industry_id INTEGER NOT NULL REFERENCES industry(industry_id),
    investment_size_id INTEGER NOT NULL REFERENCES investment_size(investment_size_id)
);

-- Investor Table
CREATE TABLE investor (
    member_id INTEGER PRIMARY KEY REFERENCES member(member_id),
    name_investor VARCHAR(255) UNIQUE NOT NULL, -- Fixed spelling from "name_inverstor"
    overview_investor VARCHAR(600) UNIQUE NOT NULL, -- Retaining UNIQUE constraint
    country_id INTEGER NOT NULL REFERENCES country(country_id),
    industry_id INTEGER NOT NULL REFERENCES industry(industry_id),
    investment_size_id INTEGER NOT NULL REFERENCES investment_size(investment_size_id)
);

-- Insert sample countries (Focusing on African nations)
INSERT INTO country (country_name) VALUES
('Nigeria'),
('Kenya'),
('South Africa'),
('Ghana'),
('Egypt'),
('Uganda'),
('Tanzania'),
('Morocco'),
('Ethiopia');

-- Insert creative industry categories
INSERT INTO industry (industry_name) VALUES
('Film & Television'),
('Fashion & Design'),
('Music & Audio Production'),
('Graphic Design & Animation'),
('Advertising & Marketing'),
('Photography'),
('Art & Fine Art');

-- Insert sample investment sizes (Private Market Impact Investments)
INSERT INTO investment_size (investment_size_name) VALUES
('Seed Funding (< $100k)'),
('Early Stage ($100k - $500k)'),
('Growth Stage ($500k - $2M)'),
('Scaling ($2M - $10M)'),
('Institutional Investment ($10M+)');

-- Insert sample members (Startup Founders & Investors)
INSERT INTO member (member_email, member_type, member_address, member_phone) VALUES
('founder@afrofilms.com', 'Startup', 'Lagos, Nigeria', 2348123456789),
('ceo@afrothreads.co.ke', 'Startup', 'Nairobi, Kenya', 254712345678),
('musicboss@afrobeatstudios.com', 'Startup', 'Cape Town, South Africa', 277812345678),
('vc@africatechfund.com', 'Investor', 'Accra, Ghana', 233501234567),
('angel@creativecapital.com', 'Investor', 'Cairo, Egypt', 201234567890);

-- Insert creative startups only
INSERT INTO startup (member_id, name_startup, overview_startup, country_id, industry_id, investment_size_id) VALUES
(1, 'Afro Films', 'A Lagos-based production house creating Pan-African films.', 1, 1, 2),
(2, 'AfroThreads', 'A Kenyan sustainable fashion brand promoting African textile heritage.', 2, 2, 3),
(3, 'Afrobeat Studios', 'A South African music production hub for Afrobeat and Amapiano artists.', 3, 3, 2);

-- Insert sample investors (Private market impact investors)
INSERT INTO investor (member_id, name_investor, overview_investor, country_id, industry_id, investment_size_id) VALUES
(4, 'Africa Tech Fund', 'VC firm focused on African startups in tech and media.', 4, 4, 3),
(5, 'Creative Capital', 'Angel investor group funding African creative startups.', 5, 2, 2);