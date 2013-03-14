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
import re
import json
import glob
import os
import hashlib
import time
import ctypes

ID_KEY = 'name'
ID_VALUE = 'value'
ID_HASH = 'hashkey'
ID_LINE = 'original_line'

ROOT_FOLDER = "../"
SOURCE_FOLDER = ROOT_FOLDER + "SmashTV/SmashTV/"
#regex find a string starting with ID_(something) = (some kind of value)
regexp = re.compile(".*(?P<key>ID_[\w]*)\s*=\s*(?P<value>[\w.]*)")

keys = []
values = []
hashkeys = []
originalLines = []
files = []


def main():
    files = findAllFilesMatchingPattern(".cs")
    for file in files:
        findIdentifierInFile(file)
    printKeysAndValues()
    id = int(time.time())
    print(id)
    toFile("workfile_" + str(id) + ".txt")
    toFile("recent.txt")
    pass

def findAllFilesMatchingPattern(pattern):
    allFiles = []
    for r,d,f in os.walk(SOURCE_FOLDER):
        for files in f:
            if files.endswith(pattern):
                 allFiles.append(os.path.join(r,files))
    return allFiles;

def findIdentifierInFile(url):
    with open(url) as f:
        anyMatch = False
        for line in f:
            match = regexp.match(line)
            if(match):
                key = match.group('key')
                print("found key {0}, is match?{1}".format(key, key in keys))
                if(not key in keys):
                    anyMatch = True
                    value = match.group('value')
                    originalLines.append(line)
                    files.append(url.replace(SOURCE_FOLDER, "$src$\\"))
                    keys.append(key)
                    values.append(value)
        if(anyMatch == False):
            print("no match in ", url)

def printKeysAndValues():
    for i, v in zip(keys,values):
        print("{0}\t\t=\t\t{1}".format(i,v))

def createHash(s):
    md5Value = int(hashlib.md5(s.encode('utf-8')).hexdigest(), 16)
    value = md5Value % 2**32 - 2**31

    print("pre={0}, post={1}".format(md5Value, value))
    return value

def anydup(thelist):
  seen = set()
  for x in thelist:
    if x in seen: return True
    seen.add(x)
  return False

def toFile(file):
    assert anydup(hashkeys) == False, "Expected hashkeys to not contain any duplicates, was:\nKeys={0}\nHash={1}".format(str(keys), str(hashkeys))

    with open(file, 'w+') as output:
        lst = []
        for key,value, line in zip(keys, values, originalLines):
            d = {}
            d[ID_KEY] = key
            d[ID_VALUE] = value
            d[ID_LINE] = line
            h = createHash(key)
            hashkeys.append(h)
            d[ID_HASH] = h
            lst.append(d)
        json.dump(lst, output)

if __name__ == '__main__':
    main()
