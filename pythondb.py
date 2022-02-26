################################################################
# A module for interacting with an existing MySQL Database
# Author: Austin Benitez
################################################################

import pymysql
from datetime import datetime
import re
import sys


class DBInteract:
    def __init__(self, dbObject=""):
        self.DBO = dbObject


    def getAllSpeeds(self):
        sql = ("SELECT * FROM speeds")
        self.DBO.cursor.execute(sql)
        return dbObject

    def getSomeSpeeds(self, start, end):
        sql = ("SELECT * FROM speeds WHERE (course_code = %s and course_prefix = %s)")
        self.DBO.cursor.execute(sql, (course_code, course_prefix))
        return dbObject

    def stripGarbage(self, response):
        stripped = response.strip("(),")

    #Given an Ip address returns the id of that adress from the iplookup table 
    def getIPID(self, ip):
        sql = ("SELECT id FROM iplookup WHERE ipv4 = '{}'".format(ip))
        self.DBO.execute(sql)
        ip_id = self.DBO.fetchall()
        return ip_id[0]

    def insertSpeed(self, st_obj):
        # get current time
        up_speed = st_obj.upload()
        down_speed = st_obj.download()
        # ping = st_obj.ping()
        curr_date_time = datetime.now()
        sql = "INSERT INTO speed VALUES ('{}',{},{});".format(curr_date_time, up_speed, down_speed)
        self.DBO.execute(sql)

    def responseToRawPing(self, response):
        raw_ping = re.search('(([0-9]+\.[0-9]+)?ms)',str(response))
        raw_ping = raw_ping[0]
        raw_ping = raw_ping.strip("ms")
        return raw_ping

    def getLatestPing(self):
        sql = ("SELECT * FROM ping ORDER BY datetime_tested DESC LIMIT 3")
        # TODO: Finish me

    def insertPing(self, ping_response, host):
        # get current time
        curr_date_time = datetime.now()
        if "Request timed out" not in str(ping_response):
            ping = self.responseToRawPing(ping_response)
        else:
            ping = -1
        ip_id = self.getIPID(host)
        sql = "INSERT INTO ping VALUES ('{}', {}, {})".format(curr_date_time, ping, ip_id[0])
        self.DBO.execute(sql)