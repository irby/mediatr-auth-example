FROM alpine:3.19.1
RUN apk add --repository http://dl-cdn.alpinelinux.org/alpine/edge/testing hurl
COPY ./tests/api .
COPY ./docker/api-test-vars.env vars.env
CMD /usr/bin/hurl --variables-file ./vars.env --test *.hurl
