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
regexpNoMatch = re.compile("^\s*\/\/.*(?P<key>ID_[\w]*)\s*=\s*(?P<value>[\w.]*)")

ID_KEY = 'name'
ID_VALUE = 'value'
ID_HASH = 'hashkey'
ID_LINE = 'original_line'

ROOT_FOLDER = "../"
SOURCE_FOLDER = ROOT_FOLDER + "SmashTV/SmashTV/"

keys = []
values = []
hashkeys = []
originalLines = []
allFiles = []
_timestamp = time.time()

def abortIfTimeout():
    if(time.time() - _timestamp > 5):
        import sys
        sys.exit("timeout error")

def main():
    readDatabase("id_dump_json.txt")
    findAllFilesMatchingPattern(".cs")
    updateIdsToHashKeys()
    pass

def findAllFilesMatchingPattern(pattern):
    for r,d,f in os.walk(SOURCE_FOLDER):
        for files in f:
            if files.endswith(pattern) and files.endswith("DataDefines.cs") == False:
                asdf = os.path.join(r,files)
                if fileContainsRegexp(asdf):
                    print(asdf)
                    allFiles.append(asdf)

def fileContainsRegexp(url):
    with open(url) as f:
        anyMatch = False
        for line in f:
            match = regexp.match(line)
            if(match):
                #skip commented lines
                if(regexpNoMatch.match(line)):
                    continue
                return True
        return False

def readDatabase(fileUrl):
    with open(fileUrl) as f:
        something = json.load(f)
        for o in something:
            keys.append(o[ID_KEY])
            values.append(o[ID_VALUE])
            hashkeys.append(o[ID_HASH])
            originalLines.append(o[ID_LINE])

def updateIdsToHashKeys():
    ix = 0
    for file in allFiles:
        for line in originalLines:
            replace(file, line, "")




#this function is probably working, for real...
#might want to rethink solution to rather save all keys in the db-file
def replace(file_path, pattern, subst):
    #Create temp file
    fh, abs_path = mkstemp()
    new_file = open(abs_path,'w')
    old_file = open(file_path)
    for line in old_file:
        new_file.write(line.replace(pattern, subst))

    #close temp file
    new_file.close()
    close(fh)
    old_file.close()

    #Remove original file
    remove(file_path)

    #Move new file
    move(abs_path, file_path)


if __name__ == '__main__':
    main()
