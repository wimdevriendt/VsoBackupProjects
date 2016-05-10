# VsoBackupProjects
Backup your code and projects stored in *TFS online*.

## Purpose :
Take a backup of your TFS online projects on a network or local folder.

## Usage :
Change the configuration file.
1. VsoUserName : your username for tfs
2. VsoPassword : your encrypted password. Use VsoBackupProjects.EncryptPasswordConsole to generate your password.
3. VsoProjects : the tfs rest api pointing to projects
(https://xxxxxxxx.visualstudio.com/DefaultCollection/_apis/projects)
4. VsoVersionControlUrl : the rest api versioncontrol url. (https://xxxxxxxxxxxxxx.visualstudio.com/<your_tfs_guid>/_api/_versioncontrol)

Change https://xxxxxxxx.visualstudio.com/ to your account url.
Change <your_tfs_guid> by your guid. You can find this guid when you right-click on a project and select 'Download as Zip'. When you click it see the address bar. It's disappearing very quickly but when you monitor it with fiddler you will see it.
More information will come.

## Roadmap :
todo

## Rotation backup 
todo