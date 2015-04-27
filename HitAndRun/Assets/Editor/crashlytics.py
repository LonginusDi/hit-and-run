import os
from sys import argv
from mod_pbxproj import XcodeProject # mod_pbxproj.py must be in same dir
path = argv[1]
print('Modifying Xcode proj at path: ' + path)
# project = XcodeProject.Load(path +'/Unity-iPhone.xcodeproj/project.pbxproj')
# project.add_other_buildsetting('COPY_PHASE_STRIP', 'YES')

# fileToAddPath = argv[2]
    #path: /Users/tuo/UnityWorkspace/XCode/PigRush-XCode-Test1    
print('post_process.py xcode build path --> ' + path)
print('post_process.py third party files path --> ' + fileToAddPath)    
    #Before execute this, you better add a check to see whether your change already exist or not, as if user
    #select *Append* rather than *Replace* in Unity when build, this will save you time and avoid duplicates. 
    
print('Step 1: add system libraries ')
    #if framework is optional, add `weak=True`
project = XcodeProject.Load(path +'/Unity-iPhone.xcodeproj/project.pbxproj')
# project.add_file('System/Library/Frameworks/CoreTelephony.framework', tree='SDKROOT')
# project.add_file('System/Library/Frameworks/MobileCoreServices.framework', tree='SDKROOT')
# project.add_file('System/Library/Frameworks/StoreKit.framework', tree='SDKROOT')
# project.add_file('System/Library/Frameworks/Social.framework', tree='SDKROOT', weak=True)
# project.add_file('usr/lib/libsqlite3.dylib', tree='SDKROOT')
project.add_file(path + "/../Crashlytics.framework", tree='SDKROOT')
project.add_file(path + "/../Fabric.framework", tree='SDKROOT')

print('Step 2: add custom libraries and native code to xcode, exclude any .meta file')
# files_in_dir = os.listdir(fileToAddPath)
# for f in files_in_dir:    
#     if not f.startswith('.'): #exclude .DS_STORE on mac
#     print f        
#     pathname = os.path.join(fileToAddPath, f)
#     fileName, fileExtension = os.path.splitext(pathname)
#     if not fileExtension == '.meta': #skip .meta file
#         if os.path.isfile(pathname):
#             print "is file : " + pathname
#             project.add_file(pathname)
#         if os.path.isdir(pathname):
#             print "is dir : " + pathname
#             project.add_folder(pathname, excludes=["^.*\.meta$"])

print('Step 3: modify the UnityAppController')
appcontroller.touch_implementation(path + '/Classes/UnityAppController.mm')
# appcontroller.touch_header(path + '/Classes/UnityAppController.h')

print('Step 4: change build setting')
project.add_other_buildsetting('GCC_ENABLE_OBJC_EXCEPTIONS', 'YES')
project.add_other_buildsetting('DEBUG_INFORMATION_FORMAT', 'dwarf-with-dsym')
project.add_other_buildsetting('DEPLOYMENT_POSTPROCESSING', 'YES')
project.add_other_buildsetting('COPY_PHASE_STRIP', 'YES')
project.add_other_buildsetting('STRIP_INSTALLED_PRODUCT', 'YES')
project.add_other_buildsetting('GCC_GENERATE_DEBUGGIN_SYMBOLS', 'YES')


print('Step 5: save our change to xcode project file')
if project.modified:
    project.backup()
    project.saveFormat3_2()