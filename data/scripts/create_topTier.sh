#!/bin/sh

api_token=[[[[Growi AccessToken]]]]
api_token2=[[[[Growi AccessToken URL Encode]]]]

url=http://growi:3000/_api/v3/pages

# ===========================================
# Function
# ===========================================

function CreateTopTier(){
topTierName=$1

# ===========================================
# Top tier page

path=/${topTierName}
body="# ${topTierName}

<div class=\\\"mx-4\\\">
    <div class=\\\"bg-info px-3 text-white\\\">List</div>
    <div class=\\\"border py-2 px-4\\\">

\$lsx(depth=1,except=_?template)

</div></div>

<br>

<div class=\\\"mx-4\\\">
    <div class=\\\"bg-info px-3 text-white\\\">Template</div>
    <div class=\\\"border py-2 px-4\\\">

\$lsx(filter=_?template)

</div></div>

"
body=${body//$'\n'/\\n}
curl -X POST \
    -H "Content-Type: application/json" \
    -d "{
    \"access_token\": \"${api_token}\",
    \"body\": \"${body}\",
    \"path\": \"${path}\",
    \"grant\": 1
    }" $url

# ===========================================
# _templae page

path=/${topTierName}/_template
body="
<div class=\\\"mx-4\\\">
    <div class=\\\"bg-warning px-3\\\">List</div>
    <div class=\\\"border py-2 px-4\\\">

\$lsx(depth=1)

</div></div>

"
body=${body//$'\n'/\\n}
curl -X POST \
    -H "Content-Type: application/json" \
    -d "{
    \"access_token\": \"${api_token}\",
    \"body\": \"${body}\",
    \"path\": \"${path}\",
    \"grant\": 1
    }" $url

# ===========================================
# _templae page

path=/${topTierName}/__template
body="
<div class=\\\"card\\\" style=\\\"max-width:80%;min-width:32rem;\\\">
<div class=\\\"card-header bg-primary\\\" data-toggle=\\\"collapse\\\" data-target=\\\"#card-collapse-1\\\"></div>
<div class=\\\"card-wrap collapse show\\\" id=\\\"card-collapse-1\\\">
<div class=\\\"card-body\\\" style=\\\"padding:0;\\\">
<div class=\\\"articleInfo\\\">

|         |                          |
| ------- | ------------------------ |
| 投稿日  | 2023/08/15               |
| 更新日  | 2023/08/15               |
| 参考URL | https://www.google.co.jp |
| 環境    | Ubuntu Server 2023       |
| 備考    | ...                      |

</div>
</div></div></div>

---

"
body=${body//$'\n'/\\n}
curl -X POST \
    -H "Content-Type: application/json" \
    -d "{
    \"access_token\": \"${api_token}\",
    \"body\": \"${body}\",
    \"path\": \"${path}\",
    \"grant\": 1
    }" $url

sleep 1
}

# ===========================================
# Main
# ===========================================

CreateTopTier Windows
CreateTopTier Linux
CreateTopTier Mac
CreateTopTier Web
CreateTopTier Application
CreateTopTier Programing
CreateTopTier Virtualization
CreateTopTier Network
CreateTopTier Storage
CreateTopTier Database
CreateTopTier Cloud

