#-------------------------------------------------------------------------------
# Name:        module1
# Purpose:      Navigate through all CS-files, find all ID_ = value-strings
#               populate these into file id_dump_json.txt in json-format
#
# Author:      MesSer
#
# Created:     11-03-2013
# Copyright:   (c) MesSer 2013
# Licence:     <your licence>
#-------------------------------------------------------------------------------
import re
import json
import glob
import os
import hashlib
import time
import ctypes

ID_NAME_KEY = 'name'
ID_VALUE = 'value'
ID_HASH = 'hashkey'
ID_ORIGINAL_LINE = 'original_line'

ROOT_FOLDER = "../"
SOURCE_FOLDER = ROOT_FOLDER + "SmashTV/SmashTV/"
#regex find a string starting with ID_(something) = (some kind of value)
regexpNoMatch = re.compile("^\s*\/\/.*(?P<key>ID_[\w]*)\s*=\s*(?P<value>[\w.]*)")
regexp = re.compile(".*(?P<key>ID_[\w]*)\s*=\s*(?P<value>[\w.]*)")

keys = []
values = []
hashkeys = []
originalLines = []
files = []


def main():
    id = int(time.time())

    addIdentifier("ID_MELEE_ENEMY_SCORE", 8)
    addIdentifier("ID_RUSHER_SCORE", 15)
    addIdentifier("ID_RANGE2_SCORE", 13)

    if(len(keys) == len(values) and len(values) > 0):
        makeHashKeys();
        print("updating file [id=" + str(id) + "]")
        backupFile("id_dump_json.txt", "id_dump_json__old")
        toFile("new_identifiers_" + str(id) + ".txt")
        appendOldData("id_dump_json__old")
        toFile("id_dump_json.txt")
    else:
        print("no new entries found!")
    pass

def addIdentifier(key, value):
    keys.append(key)
    values.append(value)
    originalLines.append("")

def printKeysAndValues():
    for i, v in zip(keys,values):
        print("{0}\t\t=\t\t{1}".format(i,v))

def makeHashKeys():
    for key in keys:
        h = createHash(key)
        hashkeys.append(h)


def createHash(s):
    md5Value = int(hashlib.md5(s.encode('utf-8')).hexdigest(), 16)
    value = md5Value % 2**32 - 2**31

    #print("pre={0}, post={1}".format(md5Value, value))
    return value

def anydup(thelist):
  seen = set()
  for x in thelist:
    if x in seen: return True
    seen.add(x)
  return False

def toFile(file):
    assert anydup(hashkeys) == False, "Hashkeys should not not contain any duplicates, was:\nKeys={0}\nHash={1}".format(str(keys), str(hashkeys))

    with open(file, 'w+') as output:
        lst = []
        for key,value,line,h in zip(keys, values, originalLines, hashkeys):
            d = {}
            d[ID_ORIGINAL_LINE] = line
            d[ID_HASH] = h
            d[ID_NAME_KEY] = key
            d[ID_VALUE] = value
            lst.append(d)
        json.dump(lst, output)

def backupFile(file1, file2):
    import shutil
    shutil.copyfile(file1, file2)

def appendOldData(fileUrl):
    try:
        with open(fileUrl) as f:
            something = json.load(f)
            for o in something:
                if(o[ID_NAME_KEY] not in keys and o[ID_HASH] not in hashkeys):
                    keys.append(o[ID_NAME_KEY])
                    values.append(o[ID_VALUE])
                    hashkeys.append(o[ID_HASH])
                    originalLines.append(o[ID_ORIGINAL_LINE])
                else:
                    print("ignoring a possible duplicate item?\n key:{0}, value:{1}, hash:{2}, originalLine:{3}".format(o[ID_NAME_KEY], o[ID_VALUE], o[ID_HASH], o[ID_ORIGINAL_LINE]))
    except:
        print("error loading old json file")

if __name__ == '__main__':
    main()
