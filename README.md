# Minge2026Spring_Team2

# コーディング規則 他
## コーディング規則

### 定数
```C#
readonly int UPPER_SNAKE_CASE;
const int UPPER_SNAKE_CASE;
static readonly int UPPER_SNAKE_CASE;
```

### Enum
```C#
public enum UpperCamelCase
{
    UPPER_SNAKE_CASE
}
```

### クラス名
```C#
public class UpperCamelCase
{

};
```

### メンバ変数名
```C#
private int lowerCamelCase;
```

### メンバ関数名
```C#
@brief 関数の説明
@param 引数の説明(この行は必要に応じて書く)
@return 返り値の説明(この行は必要に応じて書く)
public void UpperCamelCase()
{
    
}
```

※
正直、見やすければ好きなように書いてかまいません。
他人が見ても理解できるように書いてほしいです🙇‍♀️

## ブランチ命名規則
### 機能実装
feature/(自身のGitHubID)/(issueのタイトル - AddTitleなど)#(issue番号)

例)

feature/momiji1107/AddTitle#1


### バグ修正など
fix/(自身のGitHubID)/(issueのタイトル - FixTitleなど)#(issue番号)

例)

fix/momiji1107/FixTitle#2


## issueの命名規則
### タイトル
[Feature or Fix] ここにタイトルを入力(日本語で)

例)

[Feature] タイトル画面を追加

[Fix] タイトル画面を修正

### 説明
適当な説明をつけておいてください

例)

タイトル画面を作成します

タイトル画面で画像が表示されないバグを修正します

## プルリクの命名規則
### タイトル
Feat or Fix: ここにタイトルを入力(EN or 出来ればJP)

例)

Feat: Add Title

### 説明
何を実装したかを説明すればOKです。
なるべく詳細に書いてくれると嬉しいです。

例)

タイトル画面にボタンを配置しました。

ボタンをクリックするとシーンが切り替わります。
