def githubRepo = 'https://github.com/PSDekbaanbaan/API2PSSaleLocal.git'
def githubBranch = 'main'

pipeline
{
    agent any
    stages{
        stage("Git Clone")
        {
            steps
            {
                echo "========Cloning Git========"
                git url: githubRepo,
                    branch: githubBranch
            }
            post
            {
                success
                {
                    echo "========Cloning Git successfully========"
                }
                failure
                {
                    echo "========Cloning Git failed========"
                }
            }
        }
    }
}