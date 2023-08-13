# Growi CreatePage API

REST APIからページを作成  
※一括インポートの目的

最初にAPI Tokenを取得してから、

```bash
curl -s -X POST \
    -H 'Content-Type:application/json' \
    -d '{ "access_token":"[[[API TOKEN]]]", "body": "本体を記述", "path": "保存先パス", "grant": 1 }' \
    http://growi:3000/_api/v3/pages | jq ".revision._id"
```

実行例)
```bash
curl -s -X POST \
    -H 'Content-Type:application/json' \
    -d '{ "access_token":"[[[API TOKEN]]]", "body": "ページ内容の書き込み,本文,本体,,, テストてすとTEST", "path": "/TestTop/TestTest001", "grant": 1 }' \
    http://growi:3000/_api/v3/pages | jq ".revision._id"
```


``grant``パラメータについて
- 1 ⇒ 公開
- 2 ⇒ リンクを知っている人のみ
- 3 ⇒ 自分のみ or 特定グループのみ?  (検証予定無し。。。)



