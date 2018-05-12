zip -r ./Builds/webgl.zip ./Builds/WebGL/ &&
ssh USER@MYCOOLIPADDRESS.COM 'rm -rf /var/www/html/*' &&
scp ./Builds/webgl.zip USER@MYCOOLIPADDRESS.COM:/var/www/html/
ssh USER@MYCOOLIPADDRESS.COM 'cd /var/www/html/ && unzip -o webgl.zip && sudo mv /var/www/html/Builds/WebGL/* /var/www/html/ && rm webgl.zip && rm -dr Builds'