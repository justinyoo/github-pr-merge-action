#!/bin/sh -l

cd /app

dotnet restore
dotnet build src/GitHubActions.PrMerge.ConsoleApp -c Release
dotnet run --project src/GitHubActions.PrMerge.ConsoleApp -c Release -- \
    -o "$OWNER" \
    -r "$REPOSITORY" \
    -i "$ISSUE_ID" \
    -m "$MERGE_METHOD" \
    -t "$COMMIT_TITLE" \
    -d "$COMMIT_DESCRIPTION" \
    -b "$DELETE_BRANCH"
