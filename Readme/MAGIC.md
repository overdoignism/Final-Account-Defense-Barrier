# 介紹 MAGI-Crypt：Memory-hard Algorithm Guard Improve

## KDF工作流程

### 將原始 seed 分成 4 份、以 4 Thread 平行運算：

1.  1=SHA512(seed)
2.  2=Reverse(1)
3.  3=Reverse(SHA512(2))
4.  4=Reverse(SHA512(3))

### 每 Thread 進行演算：

1.  SHA512()... 迭代循序填滿 Thread's pMemory（共1048576次，64MBytes）
2.  以最後一次 SHA512 結果取 4 Bytes 產生偽隨機交換未對齊地址表 Addr1 與 Addr2
3.  依照交換表進行 32768次 SHA512(pMemory(Addr1)+pMemory(Addr2)) 再偽隨機寫回 Addr1 或 Addr2
4.  重複進行 2 + 3 步驟一共 32 次, 32768 x 32 = 1048576次
5.  將最後一次 SHA512 結果與 pMemory(0) 64bytes 再做一次 SHA512，寫回 pMemory(0)
6.  共計 2097152+ 次 SHA512

### 導出結果：

1. 將 pMemory 1~4 依序結合成為一個大 buckmemory（256 MBytes）
2. 將 buckmemory 頭尾各 32 bytes 再做一次 SHA512，再寫回 buckmemory 頭與尾
3. SHA256(buckmemory) 即完成 KDF，得到最終密鑰

## MAGI-Crypt 安全核心：

### 基礎重點：4 Thread 超過 838 萬次 SHA512，以及必要的硬性 256MB 記憶體分配

### 抗GPU/ASIC重點：每一 Thread 在 64 MBytes 內進行偽隨機未對齊地址存取，嚴重考驗記憶體存取效率與快取命中率。

### 抗量子運算重點：依賴 SHA512

### 抗側信道攻擊重點：

1. 記憶體初始為循序填滿 64MByte，沒有側信道特徵。
2. 每次產生偽隨機地址表，種子來源於 SHA512 得到的 4 Bytes，即使被側信道攻擊成功，仍有 60 Bytes 是不被曝露的，無法反推。
3. 其餘依賴 SHA512 演算法。 

## 寫完後發現，其實跟 Argon2id 很類似。殊途同歸。

