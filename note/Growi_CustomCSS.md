# Growi CustomCSS

設定 > カスタマイズ  
から行う、[カスタムCSS]の設定メモ

```css
/* リスト部分行間を狭く */
.wiki ol li, .wiki ul li {
    line-height: 1rem !important;
}

/* テーブルの行間と幅を変更 */
.wiki td, .wiki th {
    padding: 0.2rem 0.5rem !important;
}
.wiki table {
    width: auto;
    min-width: 32rem;
}

/* ドキュメントメタ情報用 */
.articleInfo {
    border: none !important;
    margin: 0.8rem 0 0 0.8rem;
}
.articleInfo td {
    border: none !important;
}
.articleInfo td:nth-child(2n+1) {
    width: 6rem;
    background-color: #C5E1FE;
}
```
