def githubRepo = 'https://github.com/PSDekbaanbaan/API2PSSaleLocal.git'
def githubBranch = 'main'

pipeline
{
    agent any
    environment
    {
        imagename = "api2PSSale:5.22003"
        dockerImage = ''
    }
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
    stage('Build Image')
    {
        steps
        {
            echo 'Building...'
        }
    }
}
