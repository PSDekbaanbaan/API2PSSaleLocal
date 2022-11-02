
pipeline
{
    agent any
    stages{
        stage("Git Clone")
        {
            steps
            {
                echo "========Cloning Git========"
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