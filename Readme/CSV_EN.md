## About CSV Import/Export

1. CSV format is a very simple format for structured data, but lacks security.
2. Unless necessary (such as exchanging between different programs), it is not recommended to use. Strongly not recommended for backup.
3. When using, be sure to check if the computer is secure.

## CSV Import

### Currently, the following software can directly support CSV input:

* keepass
* bitwarden
* 1Password8

### If you need to manually import or correct the format, the following are the basic requirements (case insensitive).

* The title field name can be "TITLE", "NAME", "ACCOUNT".
* The user field name can be "USERNAME", "LOGIN NAME", "LOGIN_USERNAME".
* The URL field name can be "URL", "LOGIN_URI", "WEB SITE".
* The password field name can be "PASSWORD", "LOGIN_PASSWORD".
* The notes field name can be "NOTES", "COMMENTS".
* The notes2 field name can be "NOTES2".
* The registered email and phone field name can be "REGDATA".

### Attenions

1. Please follow the standard CSV format specification.
2. The title, username and password fields are essential, otherwise it will be judged as a reading failure.
3. If there is an empty account title, it will be saved as "(?)".
4. This program does not support multi-line text, line breaks will be converted to ",".
5. Fields not available in this program will not be entered.
6. When importing CSV, only new accounts will be generated. Existing data will not be replaced or updated.

## CSV Export

* After exporting to CSV, the file will be stored in the application's folder with the file name Title - Date.CSV.
* After exporting, the program will automatically lead you to the folder.
* Please handle the exported CSV properly to prevent data leakage.