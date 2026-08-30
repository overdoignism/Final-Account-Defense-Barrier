# 介紹 MAGI-Crypt：Memory-hard Algorithm Guard Improve

## KDF工作流程

### V2 升級

1.  修正實際只有使用到 128MB 的 Bug
2.  地址表偽隨機數來源從隨機數產生器換成 SHA512
3.  增加攪拌次數
4.  於 3.1 版後導入（請注意，與舊版不相容。需使用CSV導出／導入功能升級）

### 將原始 seed 分成 4 份、以 4 Thread 平行運算：

1.  t1=SHA512(seed)
2.  t2=Reverse(1)
3.  t3=Reverse(SHA512(2))
4.  t4=Reverse(SHA512(3))

### 每 Thread 進行演算：

1.  SHA512()... 迭代循序填滿 Thread's pMemory（共1048576次，64MBytes）
2.  以最後一次 SHA512 結果再次迭代，每次取 40 Bytes 產生偽隨機交換未對齊地址表 Addr1 與 Addr2（共524288次）
3.  依照交換表進行 65536 次 SHA512(pMemory(Addr1)+pMemory(Addr2)) 再偽隨機寫回 Addr1 或 Addr2
4.  重複進行 2 + 3 步驟一共 32 次, 32768 x 32 = 2097152次
5.  將最後一次 SHA512 結果與 pMemory(0) 64bytes 再做一次 SHA512，寫回 pMemory(0)
6.  共計 2097152+ 次 SHA512

### 導出結果：

1. 將 pMemory 1~4 依序結合成為一個大 buckmemory（256 MBytes）
2. SHA256(buckmemory) 即完成 KDF，得到最終密鑰

## MAGI-Crypt 安全核心：

### 基礎重點

* 4 Thread 填充、產生地址表與攪拌與合計約 1468 萬次 SHA512，memory-hard 門檻為每 Thread 64MB。

### 抗GPU/ASIC重點

* 每一 Thread 在 64 MBytes 內進行偽隨機未對齊地址存取，嚴重考驗記憶體存取效率與快取命中率。

### 抗量子運算重點

* 依賴 SHA512

### 抗側信道攻擊重點

1. 記憶體初始為循序填滿 64MByte，沒有側信道特徵。
2. 位址表於每輪起始一次性產生，不依賴該輪內的中間資料，記憶體存取模式與運算過程解耦。
3. 其餘依賴 SHA512 演算法。 

## 寫完後發現，其實跟 Argon2 很類似。殊途同歸。

