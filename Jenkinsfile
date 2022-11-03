def githubRepo = 'https://github.com/PSDekbaanbaan/API2PSSaleLocal.git'
def githubBranch = 'main'

pipeline
{
    agent any
    environment
    {
        imagename = "api2pssale:5.22003"
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
        stage('Build Image')
        {
            steps
            {
                echo 'Building...'
                script
                {
                    dockerImage = docker.build imagename
                }
            }
        }
        stage('Remove Container')
        {
            steps
            {
                echo 'Remove Container...'
                script
                {
                    bat 'docker rm -f api2pssale" '
                }
            }
        }
        stage('Run Container')
        {
            steps
            {
                echo 'Run Container...'
                script
                {
                    bat 'docker run -d -p 8999:80 --name api2pssale api2pssale:5.22003 '
                }
            }
        }
    }
}
