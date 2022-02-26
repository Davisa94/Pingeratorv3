
CREATE table ipLookup(
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    time_created TIMESTAMP NOT NULL,
    ipv4 VARCHAR(15),
    domain_name VARCHAR(80));

CREATE table speed (
    speed_id INTEGER PRIMARY KEY AUTOINCREMENT,
    datetime_tested TIMESTAMP NOT NULL,
    downspeed_value DOUBLE NULL,
    upspeed_value DOUBLE NULL);

CREATE table ping (
    ping_id INTEGER PRIMARY KEY AUTOINCREMENT,
    datetime_tested TIMESTAMP NOT NULL,
    ping_value FLOAT NULL,
    ipv4_id INTEGER NOT NULL,
    CONSTRAINT ipv4_id
        FOREIGN KEY(ipv4_id)
        REFERENCES ipLookup (id));


-- ADD VALUES FOR THE IP TABLE:
INSERT INTO `iplookup` (`time_created`, `ipv4`, `domain_name`) VALUES ('2021-25-02 21:44:11', '8.8.8.8', 'google.com');
INSERT INTO `iplookup` (`time_created`, `ipv4`, `domain_name`) VALUES ('2021-25-02 21:44:11', '1.1.1.1', 'cloudflare.com');
INSERT INTO `iplookup` (`time_created`, `ipv4`, `domain_name`) VALUES ('2021-25-02 21:44:11', '208.67.222.222', 'openDNS.com');
