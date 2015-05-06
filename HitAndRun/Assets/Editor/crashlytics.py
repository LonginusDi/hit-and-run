import os
from sys import argv
import appcontroller
from mod_pbxproj import XcodeProject # mod_pbxproj.py must be in same dir
path = argv[1]
print('Modifying Xcode proj at path: ' + path)
   
print('Step 1: add system libraries ')
    #if framework is optional, add `weak=True`
project = XcodeProject.Load(path +'/Unity-iPhone.xcodeproj/project.pbxproj')
project.add_file_if_doesnt_exist(path + "/../Crashlytics.framework")
project.add_file_if_doesnt_exist(path + "/../Fabric.framework")
project.add_file_if_doesnt_exist(path + "/../libc++.1.dylib")
project.add_file_if_doesnt_exist(path + "/../libiconv.2.dylib")
project.add_file_if_doesnt_exist(path + "/../libz.1.dylib")

print('Step 2: modify the UnityAppController')
appcontroller.touch_implementation(path + '/Classes/UnityAppController.mm')
appcontroller.modify_plist(path + '/info.plist')

print('Step 3: change build setting')
project.add_other_buildsetting('GCC_ENABLE_OBJC_EXCEPTIONS', 'YES')
project.add_other_buildsetting('DEBUG_INFORMATION_FORMAT', 'dwarf-with-dsym')
project.add_other_buildsetting('DEPLOYMENT_POSTPROCESSING', 'YES')
project.add_other_buildsetting('COPY_PHASE_STRIP', 'YES')
project.add_other_buildsetting('STRIP_INSTALLED_PRODUCT', 'YES')
project.add_other_buildsetting('GCC_GENERATE_DEBUGGIN_SYMBOLS', 'YES')
project.add_other_buildsetting('SEPARATE_STRIP', 'YES')

print('Step4: modify bash script')
crash_bash_script = "if [ \"$DEBUG_INFORMATION_FORMAT\" = \"dwarf-with-dsym\" ]; then\n \
    \t./../Fabric.framework/run " + "d1dfd7c5b0fdae292e5a4cbfc409f31d8f2d3aba aa27c93efc28e337cb57610009590d1be0e2b0517e5bf4e3eda3528b07b5d92a" + "\n \
    \t./../Crashlytics.framework/run " + "d1dfd7c5b0fdae292e5a4cbfc409f31d8f2d3aba" + "\n\
else\n \
    \techo \"Not generating a dSYM, not running Crashlytics\"\n \
fi"
project.add_run_script_all_targets(crash_bash_script)
# bash_script = project.get_build_phases('Run Script');
# print (bash_script)
# 

print('Step 5: save our change to xcode project file')
if project.modified:
    project.backup()
    project.saveFormat3_2()