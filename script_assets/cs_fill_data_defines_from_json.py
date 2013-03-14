#-------------------------------------------------------------------------------
# Name:        module1
# Purpose:
#
# Author:      MesSer
#
# Created:     14-03-2013
# Copyright:   (c) MesSer 2013
# Licence:     <your licence>
#-------------------------------------------------------------------------------
import json
import os
import sys


ID_KEY = 'name'
ID_VALUE = 'value'
ID_HASH = 'hashkey'
ID_LINE = 'original_line'

ROOT_FOLDER = "../"
SOURCE_FOLDER = ROOT_FOLDER + "SmashTV/SmashTV/"
DATA_DEFINES = SOURCE_FOLDER + "Modules/DataDefines.cs"

keys = []
values = []
hashkeys = []
originalLines = []

def main():
    readDatabase("recent.txt")
    fillDataDefines(DATA_DEFINES)
    pass

def readDatabase(fileUrl):
    with open(fileUrl) as f:
        something = json.load(f)
        for o in something:
            keys.append(o[ID_KEY])
            values.append(o[ID_VALUE])
            hashkeys.append(o[ID_HASH])
            originalLines.append(o[ID_LINE])

def fillDataDefines(file):
    print(file)

    try:
        f = open(file, "w+")
        fileDump = ''.join(f.readlines())

        splitter = "class DataDefines {"

        fileStart = "using System;\nusing System.Collections.Generic;\nusing System.Linq;\nusing System.Text;\nnamespace MesserSmash.Modules {\n\tpublic static class DataDefines {\n\t\t"
        hashFunction = "private static float getValue(int hash) { return (float)SmashDb.get(hash); }\n\t\t"
        fileContent = []
        fileEnd = "\n\t}\n}"

        for k, v, h in zip(keys, values, hashkeys):
            fileContent.append("private const int _{0} = {1};".format(k, h))
            fileContent.append("public static float {0} {{ get {{ return getValue(_{0}); }} }}".format(k))

        output = fileStart + hashFunction + '\n\t\t'.join(fileContent) + fileEnd
        f.write(output)
        f.close()
        print(output)
    except IOError as e:
        print ("I/O error({0}): {1}".format(e.errno, e.strerror))
    except ValueError:
        print ("Could not convert data to an integer.")
    except:
        print ("Unexpected error:", sys.exc_info()[0])
        raise

if __name__ == '__main__':
    main()
