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

        stage('Dockerfile')
        {
            steps
            {
                echo "========Docker Building========"
                sh 'docker build -t api2pssale:5.20002.0.03 .'
            }
            post
            {
                success
                {
                    echo "========Dockerfile Build successfully========"
                }
                failure
                {
                    echo "========Dockerfile Build failed========"
                }
            }
        }
    }
}