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
keys = []
values = []

#regex find a string starting with ID_(something) = (some kind of value)
m = re.compile(".*(?P<key>ID_[\w]*)\s*=\s*(?P<value>[\w.]*)")

def main():
    showFile("../SmashTV/SmashTV/SmashTV/Arenas/Level1.cs")
    for i, v in zip(keys,values):
        print("{0}\t\t=\t\t{1}".format(i,v))
    with open('workfile', 'w+') as output:
        lst = []
        for key,value in zip(keys, values):
            d = {}
            d[key] = value
            lst.append(d)
        json.dump(lst, output)
        #json.dumps(values, output)
    pass

def showFile(url):
    with open(url) as f:
        anyMatch = False
        for line in f:
            match = m.match(line)
            if(match):
                anyMatch = True
                keys.append(match.group('key'))
                values.append(match.group('value'))
        if(anyMatch == False):
            print("no match")



if __name__ == '__main__':
    main()
