FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app

LABEL "com.github.actions.name"="GitHub PR Merge (Generic)"
LABEL "com.github.actions.description"="This action merges a PR on a given GitHub repository."
LABEL "com.github.actions.icon"="check"
LABEL "com.github.actions.color"="blue"

LABEL "repository"="http://github.com/aliencube/github-pr-merge-action"
LABEL "homepage"="http://github.com/aliencube"
LABEL "maintainer"="Justin Yoo <no-reply@aliencube.com>"

COPY *.sln .
COPY src/ ./src/

ADD entrypoint.sh /entrypoint.sh
RUN chmod +x /entrypoint.sh

ENTRYPOINT ["/entrypoint.sh"]
