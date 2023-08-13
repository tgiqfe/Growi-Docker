#!/bin/sh

api_token=[[[[Growi AccessToken]]]]
api_token2=[[[[Growi AccessToken URL Encode]]]]

url=http://growi:3000/_api/v3/pages

topTierName=Windows

# ===========================================
# Top tier page

path=/${topTierName}
body="# ${topTierName}

\$lsx(depth=1)

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
\$lsx(depth=1)

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

<table class=\\\"articleInfo\\\">
<tr class=\\\"parara\\\">
<td>更新日：</td><td>2023/08/10</td>
</tr>
<tr>
<td>投稿日：</td><td>2023/08/11<br>2023/08/12</td>
</tr>
<tr>
<td>参考URL：</td><td>https://www.google.co.jp</td>
</tr>
<tr>
<td>環境：</td><td>Windows server 2022</td>
</tr>
</tr>
<tr>
<td>備考：</td><td>...</td>
</tr>
</table>

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


