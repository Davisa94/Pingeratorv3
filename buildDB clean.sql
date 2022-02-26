CREATE table ipLookup(
    id SERIAL NOT NULL,
    time_created TIMESTAMP NOT NULL,
    ipv4 VARCHAR(15),
    domain_name VARCHAR(80),
    PRIMARY KEY(id),
    UNIQUE(id));

CREATE table speed (
    id SERIAL NOT NULL,
    datetime_tested TIMESTAMP NOT NULL,
    downspeed_value DOUBLE NULL,
    upspeed_value DOUBLE NULL
    PRIMARY KEY(id));

CREATE table ping (
    id SERIAL NOT NULL,
    datetime_tested TIMESTAMP NOT NULL,
    ping_value FLOAT NULL,
    ipv4_id BIGINT UNSIGNED NOT NULL,
    PRIMARY KEY(id),
    CONSTRAINT ipv4_id
        FOREIGN KEY(ipv4_id)
        REFERENCES ipLookup (id));

INSERT INTO `iplookup` (`time_created`, `ipv4`, `domain_name`) VALUES (now(), '8.8.8.8', 'google.com');
INSERT INTO `iplookup` (`time_created`, `ipv4`, `domain_name`) VALUES (now(), '1.1.1.1', 'cloudflare.com');
INSERT INTO `iplookup` (`time_created`, `ipv4`, `domain_name`) VALUES (now(), '208.67.222.222', 'openDNS.com');
