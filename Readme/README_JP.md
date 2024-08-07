## 宣言

1.  このプログラムはMITライセンスに基づいてライセンスされています。フリーかつオープンソースソフトウェアであり、著者は直接的または間接的な責任を負いません。ご自身で評価し、受け入れ可能であると判断された後にご使用ください。
2.  このプログラムのUIグラフィックで使用されているフォントは、「Zen Antique」と「DSEG Font Family（v0.46）」の両方が、SIL Open Font License v1.1に基づいてライセンスされています。
3.  Keepassが提供するセキュリティコンセプトと、ChatGPTが提供する一部のコードに特別な感謝を表します。

## このプログラムは誰に向いているのか？

*   いくつかのアカウントや機密情報を管理するためにアカウントパスワード（暗証番号）管理プログラムを使用したい方。
*   支払いたくない方。
*   クラウドベースのパスワードサービス、インターネットに接続されたマネージャー、またはオープンソースでないマネージャーを信頼できない方。
*   複雑な認証メカニズムが誤作動することを心配している方、追加のデバイスが故障することを心配している方。
*   インストールしたくない、すべてのデータを自分で制御およびバックアップできるようにしたい方。
*   シンプルなもの、古風な技術、伝統的な技術、クラシックな技術が好きな方。
*   エヴァンゲリオン（そのスタイル）が好きな方。
*   人気のあるマネージャーがハッカーのターゲットになっていることを心配し、人気のないマネージャーを使用したい方。
*   プログラムは非常に小さく、フロッピーディスクに入れることもできます。

## 操作ガイド

### **はじめに**

*   プログラムを準備したフォルダに置きます。
*   プログラムを開くと、暗証番号を入力するためのインタフェースが表示されます。安全で覚えやすい暗証番号を考え、2回入力し、「確認」をクリックして開始します。
*   ここで入力した暗証番号は、暗号化された「キー・シード」となり、固有の「カタログ」と直接対応します。このカタログは、複数のアカウントを管理することができます。
*   このカタログを将来開くには、単にこの暗証番号を入力します。1回入力するだけです。
*   同様に、異なるカタログのグループを追加する場合は、異なる暗証番号を入力することもできます。
*   暗証番号の代わりにファイルを選択することもできます（「ファイルを開く」を使用）。ファイル名は重要ではありませんが、ファイルの内容は一切変更できません。

### **基本操作**

*   入ったら、左側にリストがあります。一番上に「New account（新規アカウント）」があるので、それをクリックしてアカウントを追加します。
*   右のカラムにアカウント情報を入力します。意味がわからない場合は、マウスを合わせると説明が表示されます。
*   タイトル以外はすべて入力する必要はありません。入力が完了したら、「保存」をクリックしてアカウントを追加し、リストに表示されます。
*   並べ替える場合は、「上へ/下へ」ボタンを使用し、削除する場合は「削除」をクリックします。
*   別のカタログに切り替えるには、「ログアウト」ボタンを使用します。終了するには、「作動停止」ボタンをクリックします。

### **高度な操作**

*   「起動」ボタン：実行可能ファイルまたはインターネットのURLを追加します。ボタンをクリックすると、ファイルが開かれます。
*   「閱覽」ボタン：隠された情報を表示することで、のぞき見を防止します。
*   「コピー」ボタン：選択した情報をクリップボードにコピーします。
*   「移転」ボタン：このアカウントデータを別のカタログに転送できます。
*   「カタログ設定と管理」でカタログの名前を設定できます。

### **より高度な操作**

*   「自動入力のホットキー」、「自動ログアウト」、「カタログすべての転送または削除」などの機能を「カタログ設定と管理」で設定できます。
*   コピー＆ペースト自動入力のホットキーの使用に問題がある場合、個別のアカウントでオーバーライドするために「Send key」モードを使用できます。 (v1.2 以降)
*   言語ファイルの名前を「Lang\_MOD.TXT」に変更すると、対応する言語で表示されます（テキストメッセージのみ、グラフィック部分は変更されません）。
*   パラメータ「NONOTICE」を使用して、ウィンドウ下部のNOTICEを非表示にできます（非表示にする前に、一度読んでください）。
*   パラメータ「OPACITY,nn」を使用して、ウィンドウの不透明度を設定できます(100=完全に不透明で（初期設定）、初期バージョンは 93 です)。

### **仮想通貨に関連する操作**

*   BIP39パスフレーズを入力する際、二次検証は不要で、自動的に検証されます。
*   保存前に、一定レベルの仮想通貨アドレスのエラーを自動的に検出します。現在、BTC（TRX、Doge、LTC）、ETHおよびこれらの通貨の互換アドレスがサポートされています。

### **バックアップ操作／(CSV輸入輸出操作)[CSV_JP.md]**

*   メインプログラムと生成されたフォルダとファイルを単純にコピーします。圧縮して保存、USBドライブに保存、またはクラウドにアップロードすることもできます。
*   CSV入出力については、「(CSV輸入輸出)[CSV_JP.md]」の章を参照してください。

### **セキュリティ強化操作**

*   プログラム起動時に「Secure Desktop」オプションがあります。それを選択して再起動すると、セキュアデスクトップ機能が有効になります。この機能は、「SECUREDESKTOP」というパラメータを使用してショートカットに設定することもできます。
*   「RUN AS ADMIN」を選択して再起動すると、管理者特権を昇格させてプログラムをより安全にすることができます。
*   コマンドラインパラメータ SALT,xx.. を使用して塩を設定できます。（暗証番号入力画面に「塩入」と表示されます。） 

### コマンドラインパラメータのリスト

1.  「SECUREDESKTOP」セキュアデスクトップ機能を有効にします。
2.  「NONOTICE」ウィンドウ下部のNOTICEを非表示にできます。
3.  「OPACITY,nn」ウィンドウの不透明度を設定できます(100=完全に不透明で（初期設定））。
4.  「SALT,xx..」でソルトを設定できます。スペースとコンマ以外の任意の文字を使用できます。

## **セキュリティ技術に関する情報**

### **プログラムの本質的な安全性について**

1.  オープンソースであり、MITライセンスの下でライセンスされています。
2.  サードパーティのライブラリは使用しておらず、Microsoftの公式DPAPIライブラリのみを使用しています。

### **暗号化とストレージに関する情報（コアセキュリティ技術）**

1.  カタログ暗証番号はSHA256で200万回ハッシュ。これは、暗証番号を3.5桁増やしたのと同等です。（注1）
2.  最終アーカイブは、PKCS7パディング付きAES-256のCBCモードで暗号化され、ハッシュ値がIVを生成します（注2）。
3.  さまざまな長さのランダムデータがアーカイブに追加され、実際のデータと暗証番号の長さを混同します。
4.  暗証番号として任意のファイルを使用できます。最大サイズは128MBです。
5.  ファイルの自動ゼロフィル書き換え機能があります（保存/削除時）（注3）。
6.  ソルト（塩）（オプション） (注4)

### **プログラムのセキュリティ技術に関する情報（注5）**

1.  実行中の機密データの保護にはDPAPIが使用されます。
2.  メモリリーククリーニング（メモリリークの約95％を防ぎます）。
3.  キーボードやスクリーンレコーダーに耐性のあるSecure Desktopモードを利用できます (オプション)。
4.  クリップボード監視およびブロック技術が使用されます（WMインターセプト）。
5.  Memory Page Lock使用され、機密情報がスワップファイルにスワップされるのを防止します（注6）。
6.  Windows実行可能ファイルのセキュリティ緩和ポリシー：ASLR/DEP/StrictHandle/ExtensionPoint/SignaturePolicy/ImageLoad/SideChannelIsolation。
7.  ハイブリッドホットキー自動入力モードがあります。

### **操作の安全性について**

1.  アイドル状態になった場合には、自動ログアウト機能があります。
2.  パスワード欄に「コピー＆ペースト」する場合、クリップボードをクリアするかどうか確認されます。

### **備考**

1.  これが鍵導出関数（KDF）、ブルートフォース攻撃に対してのみ効果的です。レインボーテーブル攻撃に対抗するため、ソルトを使用する必要があります。
2.  IVのランダム値は元のデータハッシュから取得され、バージョン1.2以前はシステムのランダム値を使用します。
3.  特に圧縮が有効になっているなど特殊な機能を持つディスクでは、動作が保証されない。また、SSDの性質上、物理層にもデータが残ってしまうため、完全に安全な状態にするには、ディスク領域の全面書き換え（Wipe Free Spaceで検索）が必要です。
4.  このプログラムはオープンソースであるため、固定されたソルトの方法は意味をなさず、ユーザーによって決定する必要があります。また、異なるソルトの方法はカタログ間で転送することができないため、特に注意してください。
5.  このタイプのセキュリティ対策は、ハッカーやトロイの木馬の侵入を防御するためのものです。管理者権限でより効果的に動作します。**しかし、これらのセキュリティ対策は、何もしないよりはましであり、最も重要なことは、あなたのコンピュータが決してハッキングされないようにすることであることを知るべきです。**
6.  Windowsの「セキュリティポリシー」を有効にする必要があります。

## **Q&A**

*   **Q:** なぜこのソフトウェアを作成いたのですか？
*   **A:** 元々は個人的な使用のためでしたが、後で共有して、友達を作ることができるかどうかを確認することにしました。_**でも私には友達がいませんし、プログラムをテストする人すら見つけられませんでした。私の人生はとても孤独です...**_ 😭
*   **Q:** Argon2や他のアンチGPUハッシュアルゴリズムを使わずに、なぜ伝統的なSHA-256を選んだのですか？
*   **A:** 最も単純な理由は、SHA-256は組み込み関数なので、サードパーティライブラリを扱ったり、歴史的にテストされているかどうかを心配する必要がないからです。また、比較的アンチGPUであっても、ASICに対抗できるとは思えません。
*   **Q:** モバイルデバイス用やクロスプラットフォーム用のバージョンはありますか？
*   **A:** モバイルアプリの開発には詳しくなく、必要性もありませんので、ありません。
*   **Q:** オンラインウイルススキャンで、ウイルスかもしれないと言われた？
*   **A:** 普通のソフトウェアではあまり使われないセキュリティ技術を使用しているため、誤判定されることは避けられません。いずれにせよ、正しいウェブサイトURLからダウンロードし、コードをチェックして、自分でコンパイルすることが、最も安全で信頼性が高い方法です。
*   **Q:** カタログ暗証番号／ソルトを忘れた場合、どうすればよいですか？
*   **A:** それは災難です。私には何もできません。だから、カタログ暗証番号／ソルトを決して忘れないでください。
*   **Q:** ある機能を追加したり、ある使用方法を変更したりすることはできますか？
*   **A:** 基本的なバグ修正以外、他に何か変更がある可能性は低いです。何かを追加または変更したい場合は、自分で行ってください。これはオープンソースです。
*   **Q:** どんな日本語を書いていますか? それはナンセンスの束です。
*   **A:** 実は、さまざまな AI 翻訳ツールの助けを借りて、普通の正しい日本語を書くことは難しくありません。 難しいのはエヴァンゲリオンで特に好まれる漢字の使用スタイルを合わせることにあります。私は最善を尽くしました。
*   **Q:** なぜ「塩加える」ことはコマンドラインパラメータでしか行えず、暗証番号入力インターフェースでは行えないのですか？
*   **A:** 「塩加える」ことは、暗証番号の拡張として考えることができます。毎回手動で塩を入力する必要がある場合は、暗証番号を長くする方が良いです。
*   **Q:** カスタムフォントを使用してインターフェースを美しくできますか？
*   **A:** 残念ながら、セキュリティポリシーによりできません。追加のフォントファイルはプログラムのセキュリティ上の脆弱性の原因となる可能性があります。