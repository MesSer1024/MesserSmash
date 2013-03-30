#-------------------------------------------------------------------------------
# Name:        module1
# Purpose:
#
# Author:      MesSer
#
# Created:     11-03-2013
# Copyright:   (c) MesSer 2013
# Licence:     <your licence>
#-------------------------------------------------------------------------------
import json
import os
import re
import tempfile
import time

from tempfile import mkstemp
from shutil import move
from os import remove, close

#regex find a string starting with ID_(something) = (some kind of value)
regexp = re.compile(".*(?P<key>ID_[\w]*)\s*=\s*(?P<value>[\w.]*)")

ID_KEY = 'name'
ID_VALUE = 'value'
ID_HASH = 'hashkey'
ID_LINE = 'original_line'

ROOT_FOLDER = "../"
DEBUG_FOLDER = ROOT_FOLDER + "bin/debug/"
RELEASE_FOLDER = ROOT_FOLDER + "bin/release/"

keys = []
values = []
hashkeys = []
originalLines = []
allFiles = []
_timestamp = time.time()

def main():
    readJsonFile("id_dump_json.txt")
    writeDatabaseFile("database")
    pass

def readJsonFile(fileUrl):
    with open(fileUrl) as f:
        something = json.load(f)
        for o in something:
            keys.append(o[ID_KEY])
            values.append(o[ID_VALUE])
            hashkeys.append(o[ID_HASH])
            originalLines.append(o[ID_LINE])

def writeDatabaseFile(fileUrl):
    txtOutputFile = fileUrl + ".txt"
    bakOutputFile = fileUrl + "_backup.foo"
    userOutputFile = fileUrl + "_user_save.txt"
    lines = []
    for i,j,k in zip(keys, values, hashkeys):
        #remove any trailing d/f-suffixes from the numeric value
        j = j.rstrip('df')
        lines.append("{2}|{0}|{1}".format(i, j, k))

    os.rename(DEBUG_FOLDER + txtOutputFile, DEBUG_FOLDER + userOutputFile)
    os.rename(RELEASE_FOLDER + txtOutputFile, RELEASE_FOLDER + userOutputFile)
    with open(RELEASE_FOLDER + txtOutputFile, "w") as output:
        output.write("\n".join(lines))
    with open(RELEASE_FOLDER + bakOutputFile, "w") as output:
        output.write("\n".join(lines))
    with open(DEBUG_FOLDER + txtOutputFile, "w") as output:
        output.write("\n".join(lines))
    with open(DEBUG_FOLDER + bakOutputFile, "w") as output:
        output.write("\n".join(lines))



if __name__ == '__main__':
    main()
