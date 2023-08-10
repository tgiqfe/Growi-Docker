# Elasticsearch

FROM elasticsearch:8.7.1

RUN bin/elasticsearch-plugin install analysis-kuromoji
RUN bin/elasticsearch-plugin install analysis-icu

ADD elasticsearch.yml /usr/share/elasticsearch/config/elasticsearch.yml
