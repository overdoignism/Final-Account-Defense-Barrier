## Claim

1.  This program is licensed under MIT License. It is a free and open source software, so the author does not assume any direct or indirect responsibility, please evaluate it yourself and use it after you can accept it.
2.  The fonts used in the UI graphic of this program are "Zen Antique" and "DSEG Font Family (v0.46)" , both licensed under SIL Open Font License v1.1.
3.  Special thanks to Keepass for providing the security concept, and to ChatGPT for providing some of the code.

## Who is this program suitable for?

*   Want to use the account password management program to manage some accounts and confidential information.
*   Don't want to pay for it.
*   Can't trust any cloud-based password service, Internet-connected manager, or non-open source manager.
*   Worried that complex authentication mechanisms may go wrong, additional devices may fail.
*   Don't want to install, want to be able to control and back up all the data myself.
*   Love simple things, old-fashioned technology, traditional technology, classic technology.
*   Love Evangelion (with its style).
*   Afraid that the popular manager has become the target of hackers, and want to use the unpopular manager.
*   The program is so small that you can even put it on a floppy disk if you want.

## Operation Guide

### For Users Upgrading from v1.3

*   When logging in, select the Legacy KDF. After login, it's recommended to transfer to MAGI-Crypt.
*   If you wish to use non-admin permissions, run the included No_Admin.bat file and modify it as needed.

### Getting Started

*   Place this program in a prepared folder.
*   Open the program, and you¡¦ll see a password input screen. Create a secure, memorable password to use as the encryption key seed.
*   Enter it twice and press Confirm to begin.
*   The password entered here will correspond to a catalog, which can hold multiple accounts.
*   To access this catalog in the future, simply enter this password. For different catalogs, enter different passwords.
*   You can also select a file instead of a password (use Open file). The filename doesn¡¦t matter, but the file content must match exactly.

### Basic Operations

*   After entering the program, you¡¦ll see a list on the left with "New account" at the top. Click it to add a new account.
*   Then, enter your account information in the middle panel. Hovering over each field with the mouse cursor will show a detailed explanation.
*   Only the "Title" is required. Once completed, click "Save" to add it, and it will appear in the list on the left.
*   To reorder, use the "Move up/down" buttons, and hold down for continuous movement.
*   To delete, click "Delete."
*   To switch catalogs, use the "Logout" button; to exit, use the "Finish" button.

### Advanced Operations

*   "Launch" button: Add an executable file or a internet URL. Click button will open it for you.
*   "View" button: Allows you to view hidden information to prevent anyone from peeking.
*   "Copy" button: Copies the selected information to the clipboard.
*   "Transfer" button: You can transfer this account data to another catalog.
*   You can set the name of the catalog in "Catalog Setting and Management".

### More Advanced Operations

*   In "Catalog Setting", you can configure features like "Hotkey Input", "Automatic logout", "Transfer/Delete", and "CSV import/export".
*   If some websites do not allow "Copy & Paste" for input, you can specify the "Sendkey" mode for individual accounts in "Sendkey override".
*   In the login screen, enable the "A.T. Field" feature to enhance program security (Note 1).
*   Click "Security Check" to obtain a security check report.
*   If you need to run without administrator permissions, use the included No_Admin.bat file and modify it as needed.

### Cryptocurrency related Operations

*   No validation is required when using BIP39 passphrase, as it is automatically verified.
*   Before save, certain errors in cryptocurrency addresses will be automatically detected. Currently, BTC (TRX, Doge, LTC), ETH and compatible coins are supported.

### Backup operation and [CSV Import/Export](CSV_EN.md)

*  Generally, simply copy the main program along with the generated folders and files. You can choose to compress, save to a USB drive, or upload to the cloud.
*  For CSV import/export, please read the document "[CSV Inport/Export](CSV_EN.md)".

### Command Line Arguments Summary

1. "NOSECDESK" disables the secure desktop. (Use if there are conflicts with certain antivirus software)
2. "ALLOWCAP" allows screen capture. (Use when remote control software is needed)
3. "OPACITY:nn" sets window opacity (100 = fully opaque (default)).
4. "SALT:xx.." sets the salt, allowing any characters except spaces and colons.
5. "LANG:filename" loads a custom language file. (Effective for text messages only)
6. "KDF:n" sets the default KDF mode (1 = MAGI-Crypt / 2 = Legacy (v1.3) / 3 = RFC2898).

## For Developers

### About Custom Language Files

1. To create a custom language file, locate `\Final Account Defense Barrier\AllResource\Load as Bin\TXTFile\LanguageFile.csv`.
2. Use spreadsheet software to extract a column and save it as a plain text file.
3. After translating, save it as a plain text file without CSV formatting (UTF-8).
4. If there are blank lines, please keep them.

### About TXTFile.zip

* This file is used to store plain text data. It is recommended to use 7-Zip with Deflate mode, level 9, and word size 256 for compression.

## About Security Technology

### About the intrinsic safety of the program

1.  Open source, licensed under MIT license.
2.  A standalone executable file, with no third-party library used.
3.  Fully offline capable, operable in isolated system environments.

### New KDF Algorithm: MAGI-Crypt (For more details, please read [Introduction to MAGI-Crypt](MAGIC.md)(zh-TW only))

* A new KDF algorithm, "MAGI-Crypt" (Memory-hard Algorithm Guard Improve), was introduced in version 2.0.
* Enhanced from 2 million SHA-256 hashes to 8 million SHA-512 hashes.
* Adds "memory-hard" resistance to brute-force attacks, requiring 256MB memory allocation during KDF with substantial random, unaligned read/write operations.
* Execution speed is nearly unchanged compared to the previous KDF version (using 4 or more CPU cores).

### A.T. Field Protection Mode (Administrator Task Field)(Note 1)

* Scheduled via Task Scheduler to place the program in a protected state at startup, preventing it from being replaced, moved, or tampered with.
* Applies only to the executable file itself, excluding account data. (This data can only be damaged or deleted, not tampered with. To protect data, please set permissions and backups yourself.)

### About Encryption and Storage (Core Security Technology)

1.  Final storage is encrypted in AES256 CBC mode with PKCS7 padding, with the hash value generating the IV.
2.  Saved data includes variable-length random data to obfuscate the actual data and password length.
3.  Any file can be used as a password, with a maximum length of 128MB.
4.  Automatic zero-overwrite function for files (upon save/delete) (Note 2).
5.  Salt addition is optional (Note 3).

### About Program Security Technology (Note 4)

1.  Memory cleaning protects against approximately 95% of memory retention issues.
2.  Secure Desktop implemented to resist keyloggers and screen recorders (Note 5).
3.  Clipboard monitoring block technology (WM intercept) introduced.
4.  SeLockMemoryPrivilege (Note 6) and Windows error report disabled.
5.  Windows program security mitigation policy.
6.  Windows file signature verification.
7.  Screen capture is prohibited.
8.  Debugger and Loader detection.
9.  Hybrid hotkey auto-input mode.
10.  Protected with DPAPI.

### About Operating Safety Features

1.  Automatic logout function when idle.
2.  When "Copy & paste" into the password box, it will prompt you to clear the clipboard.

### Notes

1.  No actual attacks are expected against this software¡Xit's simply "Just for fun."
2.  Applies only to traditional hard drives without compression enabled. For complete security, full disk wiping (Wipe Free Space) is required.
3.  Catalogs with different salts cannot be interchanged. Please use CSV import/export for exchange as necessary.
4.  Running with administrator privileges is recommended for better protection. Most importantly, the system itself must remain secure and unbreached.
5.  If you encounter compatibility issues, refer to the "Command Line Arguments" to disable the Secure Desktop feature.
6.  Users must enable SeLockMemoryPrivilege in Windows "Security Policies." Honestly, its effectiveness is unclear; for Windows, this setting may be merely advisory.

## **Q&A**

*   **Q:** Why did you write this software?
*   **A:** Originally it was for my personal use, but later I decided to share it and see if I could make some friends.­
*   **Q:** Why not use a GPU-resistant hash like Argon2 and instead write MAGI-Crypt?
*   **A:** The simplest reason is that SHA-512 is a built-in function, so there¡¦s no need to handle third-party libraries or add DLLs.
*   **Q:** Will there be a version for mobile devices or cross-platform use?
*   **A:** I am not familiar with mobile app development and have no need for it, so there won't be one.
*   **Q:** The online virus scanning said there are viruses?
*   **A:** Because I used some security techniques that are less commonly used in ordinary software, being misjudged is inevitable. In any case, downloading from the correct website URL, checking the code, compiling it yourself, is the safest and most reliable.
*   **Q:** What should I do if I forget the catalog password / salt?
*   **A:** Well, that's a disaster. There's nothing I can do for you. So never forget the catalog password / salt.
*   **Q:** Can you add a certain feature or change a certain usage?
*   **A:** Unless the software has a very basic bug, this is probably it. If you want to add or change something, please do it yourself. This is open source.
*   **Q:** What kind of Japanese are you writing? It's just a bunch of nonsense.
*   **A:** Actually, with the help of various AI translation tools, it's not difficult to write normal and correct Japanese. The difficulty lies in matching the style of using kanji that is particularly favored in Evangelion. I have tried my best.
*   **Q:** Why can salting only be done through command line parameters and not in the password input interface?
*   **A:** "Salting" can be thought of as an additional extension to the password. If you have to manually enter the salt each time, it's better to just make the password longer.
*   **Q:** Is it possible to customize fonts to beautify the interface?
*   **A:** Unfortunately, it's not feasible due to security policies. Additional font files could potentially introduce security vulnerabilities to the program.
