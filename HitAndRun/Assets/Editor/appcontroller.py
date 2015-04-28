    #!/usr/bin/python
import sys
import os

def process_app_controller_wrapper(appcontroller_filename, newContent, methodSignatures, valuesToAppend, positionsInMethod, contentToAppend=None):
    appcontroller = open(appcontroller_filename, 'r')
    lines = appcontroller.readlines()
    appcontroller.close()
    foundWillResignActive = False    
    foundIndex = -1
    for line in lines:         
        print line
        newContent += line
        for idx, val in enumerate(methodSignatures):
            print idx, val
            if (line.strip() == val):
                print "founded match method: " + val
                foundIndex = idx
                foundWillResignActive = True
        if foundWillResignActive :
            if positionsInMethod[foundIndex] == 'begin' and line.strip() == '{':
                print "add code to resign body"
                newContent += ("\n\t" + valuesToAppend[foundIndex] + "\n\n")
                foundWillResignActive = False
            if 	positionsInMethod[foundIndex] == 'end' and line.strip() == '}':
                newContent = newContent[:-3]
                newContent += ("\n\n\t" + valuesToAppend[foundIndex] + "\n")
                newContent += "}\n"
                foundWillResignActive = False
        if line.strip() == '@end' and (not contentToAppend is None):
                newContent = newContent[:-6]
                newContent += ("\n\n\t" + contentToAppend + "\n")
                newContent += "@end"                            
            
    print "-------done loop close stream and content: " + newContent                    
    appcontroller = open(appcontroller_filename, 'w')    
    appcontroller.write(newContent)
    appcontroller.close()        


def importHeaders():
    return '''
#import <Fabric/Fabric.h>
#import <Crashlytics/Crashlytics.h>
'''

def extraCodeToAddInAppControllerMMFile():
    return ''''''

    
def touch_implementation(appcontroller_filename):
    # appcontroller = open(appcontroller_filename, 'w')
    # print(" process AppController.mm add imports header")
    newContent = importHeaders()
     
    #starting line of method {
    methodSignatures = []
    #value to append near }
    valueToAppend = []
	#position to add insert at the beginning o
    positionsInMethod = []
    
    methodSignatures.append('- (BOOL)application:(UIApplication*)application didFinishLaunchingWithOptions:(NSDictionary*)launchOptions')
    valueToAppend.append('[Fabric with:@[CrashlyticsKit]];')
    positionsInMethod.append("begin")

    process_app_controller_wrapper(appcontroller_filename, newContent, methodSignatures, valueToAppend, positionsInMethod, extraCodeToAddInAppControllerMMFile())    

