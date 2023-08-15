# Growi CustomCSS

設定 > カスタマイズ  
から行う、[カスタムCSS]の設定メモ

```css
/* リスト部分行間を狭く (lsxのページリストの行間は少し広く) */
.wiki ol li, .wiki ul li {
    line-height: 1rem !important;
}
.lsx ul li {
    line-height: 1.8em !important;
}

/* テーブルの行間と幅を変更 */
.wiki td, .wiki th {
    padding: 0.2rem 0.5rem !important;
}
.wiki table {
    width: auto;
    min-width: 32rem;
}

/* コドハイライトの上下空白を狭く */
.wiki p {
    margin-top: 5px !important;
    margin-bottom: 5px !important;
}
.code-highlighted {
    margin-top: 0 !important;
    margin-bottom: 0 !important;
}

/* ドキュメントメタ情報用 */
.articleInfo {
    margin: 0.8rem 0 0 0.8rem;
}
.articleInfo table {
    width: 97%;
    min-width:30rem;
    border: none;
}
.articleInfo th {
    display: none;
}
.articleInfo td {
    border: none !important;
}
.articleInfo td:nth-child(2n+1) {
    width: 5rem;
    background-color: #C5E1FE;
    font-weight: bold;
}
```


