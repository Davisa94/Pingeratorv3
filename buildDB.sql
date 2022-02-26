use pingerator;

-- Updated manualy
-- ip lookup:
    -- id
    -- test_time_id fk > testtime table
    -- domain name
    -- 
CREATE table ipLookup(
    id SERIAL NOT NULL,
    time_created TIMESTAMP NOT NULL,
    ipv4 VARCHAR(15),
    domain_name VARCHAR(80),
    PRIMARY KEY(id),
    UNIQUE(id));
-- incidint table

-- testtime table 
    -- id primary
    -- datetime
    -- 

-- CREATE table testTime(
--     id SERIAL NOT NULL,
--     datetime_tested TIMESTAMP NOT NULL,
--     PRIMARY KEY(id),
--     UNIQUE(id));

-- We need a table for the speeds
    -- test_time_id fk > testtime table
    -- we need speed value
    -- we need ip address
CREATE table speed (
    datetime_tested TIMESTAMP NOT NULL,
    downspeed_value DOUBLE NULL,
    upspeed_value DOUBLE NULL);

CREATE table ping (
    datetime_tested TIMESTAMP NOT NULL,
    ping_value FLOAT NULL,
    ipv4_id BIGINT UNSIGNED NOT NULL,
    CONSTRAINT ipv4_id
        FOREIGN KEY(ipv4_id)
        REFERENCES ipLookup (id));
-- we need a table for the pings
    -- test_time_id fk > testtime table
    -- we need ping response time or NULL if none
    -- we need id of domain name given the appropriate IP


-- ADD VALUES FOR THE IP TABLE:
INSERT INTO `pingerator`.`iplookup` (`time_created`, `ipv4`, `domain_name`) VALUES (now(), '8.8.8.8', 'google.com');
INSERT INTO `pingerator`.`iplookup` (`time_created`, `ipv4`, `domain_name`) VALUES (now(), '1.1.1.1', 'cloudflare.com');
INSERT INTO `pingerator`.`iplookup` (`time_created`, `ipv4`, `domain_name`) VALUES (now(), '208.67.222.222', 'openDNS.com');
