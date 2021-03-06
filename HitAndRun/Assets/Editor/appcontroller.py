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

def append_xml(appcontroller_filename, newContent, contentToAppend=None):
    appcontroller = open(appcontroller_filename, 'r')
    lines = appcontroller.readlines()
    appcontroller.close()
    foundWillResignActive = False    
    count = 0
    for line in lines:         
        print line
        if (count < 3):
            count = count + 1
        else:
            newContent += line
                        
            
    print "-------done info.plist loop close stream and content: " + newContent                    
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
    # valueToAppend.append('[Fabric with:@[CrashlyticsKit]];')
    valueToAppend.append('[[Crashlytics sharedInstance] setDebugMode:YES]; \n  [Fabric with:@[CrashlyticsKit]]; ');
    positionsInMethod.append("begin")

    process_app_controller_wrapper(appcontroller_filename, newContent, methodSignatures, valueToAppend, positionsInMethod, extraCodeToAddInAppControllerMMFile())    

def add_fabric():
    return''' 
<?xml version="1.0" encoding="utf-8"?>
<plist version="1.0">
  <dict>
    <key>Fabric</key>
        <dict>
            <key>APIKey</key>
            <string>d1dfd7c5b0fdae292e5a4cbfc409f31d8f2d3aba</string>
            <key>Kits</key>
            <array>
                <dict>
                    <key>KitInfo</key>
                    <dict/>
                    <key>KitName</key>
                    <string>Crashlytics</string>
                </dict>
            </array>
        </dict>
'''

def modify_plist(appcontroller_filename):
    # appcontroller = open(appcontroller_filename, 'w')
    # print(" process AppController.mm add imports header")
    newContent = add_fabric()
     
    #starting line of method {
    #value to append near }
    
    append_xml(appcontroller_filename, newContent, extraCodeToAddInAppControllerMMFile())    
